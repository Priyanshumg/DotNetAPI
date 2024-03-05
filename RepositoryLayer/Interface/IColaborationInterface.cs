using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IColaborationInterface
    {
        public ColabEntity AddColaboratory(int userIdToColab, int NoteIdToColab);
        public NotesEntity addNote(int noteIdToColab, int userIdToColab);
        public ColabEntity RemoveColaborator(int NoteIdToRemoveFromColab, int UserIdToRemoveFromColab);
    }
}
