﻿using System.ComponentModel;

namespace OpenTracker.Models.UndoRedo
{
    public interface IUndoRedoManager : INotifyPropertyChanged
    {
        bool CanRedo { get; }
        bool CanUndo { get; }

        void NewAction(IUndoable action);
        void Redo();
        void Reset();
        void Undo();
    }
}