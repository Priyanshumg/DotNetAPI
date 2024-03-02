using CommonLayer.RequestModel.NotesModel;
using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
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
    }
}
