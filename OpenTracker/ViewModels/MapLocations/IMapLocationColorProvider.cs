using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using OpenTracker.Models.Locations;
using ReactiveUI;

namespace OpenTracker.ViewModels.MapLocations
{
    public interface IMapLocationColorProvider : INotifyPropertyChanged
    {
        string BorderColor { get; }
        string Color { get; }
        ReactiveCommand<PointerEventArgs, Unit> HandlePointerEnter { get; }
        ReactiveCommand<PointerEventArgs, Unit> HandlePointerLeave { get; }

        delegate IMapLocationColorProvider Factory(IMapLocation mapLocation);
    }
}