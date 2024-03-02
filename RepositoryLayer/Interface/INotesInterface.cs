using CommonLayer.RequestModel.NotesModel;
using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesInterface
    {
        public NotesEntity CreateNote(CreateNotes model, int Id);
        public List<NotesEntity> GetNote(int id);
    }
}
