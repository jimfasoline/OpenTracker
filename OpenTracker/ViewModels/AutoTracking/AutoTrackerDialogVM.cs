﻿using Avalonia.Threading;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Utils.Dialog;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.AutoTracking
{
    public class AutoTrackerDialogVM : DialogViewModelBase, IAutoTrackerDialogVM
    {
        private readonly IAutoTracker _autoTracker;
        private readonly DispatcherTimer _memoryCheckTimer;
        private int _tickCount;

        private string _uriString = "ws://localhost:8080";
        public string UriString
        {
            get => _uriString;
            set => this.RaiseAndSetIfChanged(ref _uriString, value);
        }

        public IEnumerable<string>? Devices =>
            _autoTracker.Devices;

        private string? _device;
        public string? Device
        {
            get => _device;
            set => this.RaiseAndSetIfChanged(ref _device, value);
        }

        public bool RaceIllegalTracking =>
            _autoTracker.RaceIllegalTracking;

        private bool _canGetDevices;
        public bool CanGetDevices
        {
            get => _canGetDevices;
            private set => this.RaiseAndSetIfChanged(ref _canGetDevices, value);
        }

        private bool _canStop;
        public bool CanStop
        {
            get => _canStop;
            private set => this.RaiseAndSetIfChanged(ref _canStop, value);
        }

        private bool _canStart;
        public bool CanStart
        {
            get => _canStart;
            private set => this.RaiseAndSetIfChanged(ref _canStart, value);
        }

        public IAutoTrackerLogVM Log { get; }
        public IAutoTrackerStatusVM Status { get; }

        public ReactiveCommand<Unit, Unit> GetDevicesCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> StartCommand { get; }

        public ReactiveCommand<Unit, Unit> ToggleRaceIllegalTrackingCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isGettingDevices;
        public bool IsGettingDevices =>
            _isGettingDevices.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStopping;
        public bool IsStopping =>
            _isStopping.Value;

        private readonly ObservableAsPropertyHelper<bool> _isStarting;
        public bool IsStarting =>
            _isStarting.Value;

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoTrackerDialogVM(IAutoTracker autoTracker, IAutoTrackerStatusVM status,
            IAutoTrackerLogVM log)
        {
            _autoTracker = autoTracker;

            Status = status;
            Log = log;

            _memoryCheckTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            _memoryCheckTimer.Tick += OnMemoryCheckTimerTick;

            ToggleRaceIllegalTrackingCommand = ReactiveCommand.Create(ToggleRaceIllegalTracking);

            GetDevicesCommand = ReactiveCommand.CreateFromTask(
                GetDevices, this.WhenAnyValue(x => x.CanGetDevices));
            GetDevicesCommand.IsExecuting.ToProperty(
                this, x => x.IsGettingDevices, out _isGettingDevices);

            StopCommand = ReactiveCommand.CreateFromTask(
                Stop, this.WhenAnyValue(x => x.CanStop));
            StopCommand.IsExecuting.ToProperty(
                this, x => x.IsStopping, out _isStopping);

            StartCommand = ReactiveCommand.CreateFromTask(
                Start, this.WhenAnyValue(x => x.CanStart));
            StartCommand.IsExecuting.ToProperty(
                this, x => x.IsStarting, out _isStarting);

            PropertyChanged += OnPropertyChanged;
            _autoTracker.PropertyChanged += OnAutoTrackerChanged;
            _autoTracker.SNESConnector.PropertyChanged += OnConnectorChanged;

            UpdateCanGetDevices();
            UpdateCanStart();
            UpdateCanStop();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on itself.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UriString))
            {
                UpdateCanGetDevices();
            }

            if (e.PropertyName == nameof(Device))
            {
                UpdateCanStart();
            }
        }

        /// <summary>
        /// Subscribes to the dispatcher timer and triggers the appropriate memory checks on tick.
        /// </summary>
        /// <param name="sender">
        /// The event sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void OnMemoryCheckTimerTick(object? sender, EventArgs e)
        {
            _tickCount++;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _autoTracker.InGameCheck();

                if (_tickCount == 3)
                {
                    _tickCount = 0;
                    _autoTracker.MemoryCheck();
                }
            });
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISNESConnector interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnConnectorChanged(object? sender, PropertyChangedEventArgs e)
        {
            UpdateCanGetDevices();
            UpdateCanStart();
            UpdateCanStop();

            if (e.PropertyName == nameof(ISNESConnector.Status) &&
                _autoTracker.SNESConnector.Status == ConnectionStatus.Error)
            {
                _memoryCheckTimer.Stop();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AutoTracker class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTracker.RaceIllegalTracking))
            {
                this.RaisePropertyChanged(nameof(RaceIllegalTracking));
            }

            if (e.PropertyName == nameof(IAutoTracker.Devices))
            {
                this.RaisePropertyChanged(nameof(Devices));
            }
        }

        /// <summary>
        /// Updates the CanGetDevices property with a value representing whether the SNES connector
        /// can be queries for a device list.
        /// </summary>
        private void UpdateCanGetDevices()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanGetDevices = CanCreateWebSocketUri() &&
                    (_autoTracker.SNESConnector.Socket == null ||
                    _autoTracker.SNESConnector.Status == ConnectionStatus.Connected);
            });
        }

        /// <summary>
        /// Sets the value of the Devices property to an observable collection of strings representing
        /// the devices returns by the SNES connector.
        /// </summary>
        /// <returns>
        /// An observable representing the result of the command.
        /// </returns>
        private async Task GetDevices()
        {
            if (_autoTracker.SNESConnector.Uri != UriString)
                {
                    _autoTracker.SNESConnector.Uri = UriString;
                }
            
            await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    await _autoTracker.GetDevices();
                });
        }

        /// <summary>
        /// Updates the CanStop property with a value representing whether autotracking
        /// can be stopped.
        /// </summary>
        private void UpdateCanStop()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanStop = _autoTracker.SNESConnector.Socket != null;
            });
        }

        /// <summary>
        /// Stops autotracking.
        /// </summary>
        private async Task Stop()
        {
            _memoryCheckTimer.Stop();
            await _autoTracker.Stop();
        }

        /// <summary>
        /// Updates the CanStart property with a value representing whether autotracking
        /// can be started.
        /// </summary>
        private void UpdateCanStart()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                CanStart = _autoTracker.SNESConnector.Socket != null &&
                    _autoTracker.SNESConnector.Status != ConnectionStatus.Error &&
                    Device != null && _autoTracker.SNESConnector.Device != Device;
            });
        }

        /// <summary>
        /// Starts autotracking.
        /// </summary>
        private async Task Start()
        {
            _autoTracker.SNESConnector.Device = Device ??
                throw new NullReferenceException();
            
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _memoryCheckTimer.Start();
            });
        }

        /// <summary>
        /// Toggles the race illegal tracking flag.
        /// </summary>
        private void ToggleRaceIllegalTracking()
        {
            _autoTracker.RaceIllegalTracking = !_autoTracker.RaceIllegalTracking;
        }

        /// <summary>
        /// Returns whether the UriString property value is a valid URI to be accepted by the web
        /// socket library.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the UriString property value can be accepted by the
        /// web socket library.
        /// </returns>
        private bool CanCreateWebSocketUri()
        {
            if (!Uri.IsWellFormedUriString(UriString, UriKind.Absolute))
            {
                return false;
            }

            var uri = new Uri(UriString);

            if (uri == null)
            {
                return false;
            }

            if (!uri.IsAbsoluteUri)
            {
                return false;
            }

            var schm = uri.Scheme;

            if (!(schm == "ws" || schm == "wss"))
            {
                return false;
            }

            var port = uri.Port;

            if (port == 0)
            {
                return false;
            }

            if (uri.Fragment.Length > 0)
            {
                return false;
            }

            return true;
        }
    }
}
