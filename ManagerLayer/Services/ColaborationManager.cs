using ManagerLayer.Interface;
using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class ColaborationManager : IColaborationManager
    {
        public IColaborationInterface repository;

        public ColaborationManager(IColaborationInterface repository)
        {
            this.repository = repository;
        }
        public ColabEntity AddColaboratory(int userIdToColab, int NoteIdToColab)
        {
            return repository.AddColaboratory(userIdToColab, NoteIdToColab);
        }
        public NotesEntity addNote(int noteIdToColab, int userIdToColab)
        {
            return repository.addNote(noteIdToColab, userIdToColab);
        }
        public ColabEntity RemoveColaborator(int NoteIdToRemoveFromColab, int UserIdToRemoveFromColab)
        {
            return repository.RemoveColaborator(NoteIdToRemoveFromColab, UserIdToRemoveFromColab);
        }
    }
}
