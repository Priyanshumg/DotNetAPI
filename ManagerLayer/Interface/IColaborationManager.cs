using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IColaborationManager
    {
        public ColabEntity AddColaboratory(int userIdToColab, int NoteIdToColab);
        public NotesEntity addNote(int noteIdToColab, int userIdToColab);
    }
}
