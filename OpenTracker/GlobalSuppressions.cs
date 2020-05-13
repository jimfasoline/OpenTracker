﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.BossControlVM.ImageSource")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.DungeonPrizeControlVM.ImageSource")]
[assembly: SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "Databinding to TextBlock requires a string.", Scope = "member", Target = "~P:OpenTracker.ViewModels.AutoTrackerDialogVM.URIString")]
[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Future development required for localization.", Scope = "member", Target = "~M:OpenTracker.ViewModels.MainWindowVM.OpenResetDialog~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.MapControlVM.ImageSource")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.MapLocationControlVM.ImageSource")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.MarkingSelectControlVM.ImageSource")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.SectionControlVM.MarkingSource")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.ColorSelectDialog.AccessibilityInspectColorPickerOpenProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.ColorSelectDialog.AccessibilityNoneColorPickerOpenProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.ColorSelectDialog.AccessibilityNormalColorPickerOpenProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.ColorSelectDialog.AccessibilityPartialColorPickerOpenProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.ColorSelectDialog.AccessibilitySequenceBreakColorPickerOpenProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.ColorSelectDialog.EmphasisFontColorPickerOpenProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.CurrentFilePathProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.ItemsPanelMarginProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.LocationsPanelMarginProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.MapMarginProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.MapPanelOrientationProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.ModeSettingsPopupOpenProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.UIPanelDockProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.UIPanelHorizontalAlignmentProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.UIPanelOrientationDockProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.UIPanelVerticalAlignmentProperty")]
[assembly: SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "ViewLocator is a canned Avalonia class and will not be edited.", Scope = "member", Target = "~M:OpenTracker.ViewLocator.Build(System.Object)~Avalonia.Controls.IControl")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "ViewLocator is a canned Avalonia class and will not be edited.", Scope = "member", Target = "~M:OpenTracker.ViewLocator.Build(System.Object)~Avalonia.Controls.IControl")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~M:OpenTracker.ViewModels.ItemControlVM.#ctor(OpenTracker.ViewModels.UndoRedoManager,OpenTracker.Models.AppSettings,OpenTracker.Models.Game,OpenTracker.Models.Item[])")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.SectionControlVM.ImageSource")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Filenames are lower case.", Scope = "member", Target = "~P:OpenTracker.ViewModels.SectionControlVM.BossImageSource")]
[assembly: SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "ObservableDictionary is a dictionary-type class.", Scope = "type", Target = "~T:OpenTracker.Utils.ObservableDictionary`2")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Necessary use of static public field.", Scope = "member", Target = "~F:OpenTracker.App.Selector")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.SelectorProperty")]
[assembly: SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "AvaloniaProperty must be public fields for binding.", Scope = "member", Target = "~F:OpenTracker.Views.MainWindow.LocationsPanelOrientationProperty")]
