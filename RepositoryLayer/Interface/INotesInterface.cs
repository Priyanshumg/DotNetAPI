﻿using CommonLayer.RequestModel.NotesModel;
using RepositoryLayer.Enitity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesInterface
    {
        public NotesEntity CreateNote(CreateNotes model, int Id);
        public List<NotesEntity> GetAllNote(int id);
        public NotesEntity UpdateNote(int NotesId, UpdateNotesModel model);
        public NotesEntity Trash(int NotesId);
        public NotesEntity DeleteNoteOperation(int NotesId);
        public NotesEntity Archive(int NotesId);
        public NotesEntity Colour(CreateNotes model, int NotesId);
        public NotesEntity Reminder(CreateNotes model,int NotesId);
        public string UploadImage(string filepath, int NotesId, int Id);
    }

    public interface ILabelNotesRepository
    {

    }
}
