﻿using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to add a note to a location.
    /// </summary>
    public class AddNote : IUndoable
    {
        private readonly IMarking.Factory _factory;
        private readonly ILocation _location;
        private IMarking? _note;

        public delegate AddNote Factory(ILocation location);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// An Autofac factory for creating markings.
        /// </param>
        /// <param name="location">
        /// The location to which the note will be added.
        /// </param>
        public AddNote(IMarking.Factory factory, ILocation location)
        {
            _factory = factory;
            _location = location;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _location.Notes.Count < 4;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _note = _factory();
            _location.Notes.Add(_note);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _location.Notes.Remove(_note!);
        }
    }
}
