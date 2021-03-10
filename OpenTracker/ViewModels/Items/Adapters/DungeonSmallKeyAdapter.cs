using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Items;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters
{
    /// <summary>
    /// This class contains the logic to adapt dungeon small key data to an item control.
    /// </summary>
    public class DungeonSmallKeyAdapter : ViewModelBase, IItemAdapter
    {
        private readonly IColorSettings _colorSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IItem _item;

        public string ImageSource { get; } = "avares://OpenTracker/Assets/Images/Items/smallkey0.png";
        public string? Label =>
            _item.Current == 0 ? null :
                _item.Current.ToString(CultureInfo.InvariantCulture) + (_item.CanAdd() ? "" : "*");
        public string LabelColor => _item.CanAdd() ? "#ffffffff" : _colorSettings.EmphasisFontColor;
        
        public IBossSelectPopupVM? BossSelect { get; } = null;
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate DungeonSmallKeyAdapter Factory(IItem item);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="colorSettings">
        /// The color settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public DungeonSmallKeyAdapter(
            IColorSettings colorSettings, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            IItem item)
        {
            _colorSettings = colorSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _item = item;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _item.PropertyChanged += OnItemChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    this.RaisePropertyChanged(nameof(ImageSource));
                    this.RaisePropertyChanged(nameof(Label));
                    this.RaisePropertyChanged(nameof(LabelColor));
                });
            }
        }

        /// <summary>
        /// Creates an undoable action to add an item and sends it to the undo/redo manager.
        /// </summary>
        private void AddItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetAddItem(_item));
        }

        /// <summary>
        /// Creates an undoable action to remove an item and sends it to the undo/redo manager.
        /// </summary>
        private void RemoveItem()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetRemoveItem(_item));
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    AddItem();
                    break;
                case MouseButton.Right:
                    RemoveItem();
                    break;
            }
        }
    }
}