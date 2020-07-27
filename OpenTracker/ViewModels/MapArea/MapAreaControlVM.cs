﻿using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Connections;
using OpenTracker.ViewModels.MapArea.MapLocations;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace OpenTracker.ViewModels.MapArea
{
    /// <summary>
    /// This is the ViewModel of the map area control.
    /// </summary>
    public class MapAreaControlVM : ViewModelBase
    {
        private readonly MainWindowVM _mainWindow;

        public Orientation MapPanelOrientation => 
            AppSettings.Instance.MapOrientation ?? _mainWindow.Orientation;

        public ObservableCollection<MapVM> Maps { get; }
        public ObservableCollection<MapConnectionVM> Connectors { get; } =
            new ObservableCollection<MapConnectionVM>();
        public ObservableCollection<MapLocationVMBase> MapLocations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainWindow">
        /// The main window ViewModel parent class.
        /// </param>
        public MapAreaControlVM(MainWindowVM mainWindow)
        {
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            Maps = MapAreaControlVMFactory.GetMapControlVMs(this);
            MapLocations = MapAreaControlVMFactory.GetMapLocationControlVMs(
                this, mainWindow.UIPanel.LocationsPanel.Locations);

            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
            ConnectionCollection.Instance.CollectionChanged += OnConnectionsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.MapOrientation))
            {
                UpdateMapOrientation();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MainWindowVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.Orientation))
            {
                UpdateMapOrientation();
            }
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the ObservableCollection of map entrance
        /// connections.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnConnectionsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object item in e.NewItems)
                {
                    Connectors.Add(new MapConnectionVM((Connection)item, this));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object item in e.OldItems)
                {
                    Connection connection = (Connection)item;

                    foreach (MapConnectionVM connector in Connectors)
                    {
                        if (connector.Connection == connection)
                        {
                            Connectors.Remove(connector);
                            break;
                        }
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Connectors.Clear();

                foreach (Connection connection in (ObservableCollection<Connection>)sender)
                {
                    Connectors.Add(new MapConnectionVM(connection, this));
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the MapPanelOrientation property.
        /// </summary>
        private void UpdateMapOrientation()
        {
            this.RaisePropertyChanged(nameof(MapPanelOrientation));
        }
    }
}