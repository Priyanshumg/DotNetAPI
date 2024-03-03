using CommonLayer.RequestModel.NotesModel;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface INotesManager
    {
        public NotesEntity CreateNote(CreateNotes model, int Id);
        public List<NotesEntity> GetAllNote(int id);
        public NotesEntity UpdateNote(int NotesId, UpdateNotesModel model);
        public NotesEntity Trash(int NotesId);
        public NotesEntity DeleteNoteOperation(int NotesId, int id);
        public NotesEntity Archive(int NotesId);
        public NotesEntity Colour(int NotesId);
        public NotesEntity Reminder(int NotesId);
    }
}
