using CommonLayer.RequestModel.NotesModel;
using ManagerLayer.Interface;
using Repository.Services;
using RepositoryLayer.Enitity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class NotesManager : INotesManager
    {
        private readonly INotesInterface repository;
        public NotesManager(INotesInterface repository)
        {
            this.repository = repository;
        }
        public NotesEntity CreateNote(CreateNotes model, int Id)
        {
            return repository.CreateNote(model, Id);
        }
        public List<NotesEntity> GetAllNote(int id)
        {
            return repository.GetAllNote(id);
        }
        public NotesEntity UpdateNote(int NotesId, UpdateNotesModel model)
        {
            return repository.UpdateNote(NotesId, model);
        }
        public NotesEntity Trash(int NotesId)
        {
            return repository.Trash(NotesId);
        }
        public NotesEntity DeleteNoteOperation(int NotesId, int id)
        {
            return repository.DeleteNoteOperation(NotesId, id);
        }
        public NotesEntity Archive(int NotesId)
        {
            return repository.Archive(NotesId);
        }
        public NotesEntity Colour(int NotesId)
        {
            return repository.Colour(NotesId);
        }
        public NotesEntity Reminder(int NotesId)
        {
            return repository.Reminder(NotesId);
        }

    }
}
