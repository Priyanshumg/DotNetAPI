using CommonLayer.RequestModel.NotesModel;
using ManagerLayer.Interface;
using Repository.Services;
using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class NoteManager : INotesManager
    {
        private readonly INotesInterface repository;
        public NoteManager(INotesInterface repository)
        {
            this.repository = repository;
        }
        public NotesEntity CreateNote(CreateNotes model, int Id)
        {
            return repository.CreateNote(model, Id);
        }
    }

}
