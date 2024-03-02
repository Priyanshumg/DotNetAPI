using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.RequestModel.NotesModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Repository.Services
{
    public class NoteRepository : INotesInterface
    {
        private readonly UserContext context;
        public NoteRepository(UserContext context)
        {
            this.context = context;
        }
        public NotesEntity CreateNote(CreateNotes model, int Id)
        {
            NotesEntity entity = new NotesEntity();
            entity.NotesId = Id;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Colour = model.Colour;
            entity.Image = model.Image;
            entity.IsArchive = model.IsArchive;
            entity.IsPin = model.IsPin;
            entity.ISTrash = model.IsTrash;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            context.NotesTable.Add(entity);
            context.SaveChanges();
            return entity;
        }
        public List<NotesEntity> GetAllNote(int id)
        {
            return context.NotesTable.Where<NotesEntity>(a => a.UserId == id).ToList();
        }

        public NotesEntity UpdateNote(int NotesId, UpdateNotesModel model)
        {
            var noteToUpdate = context.NotesTable.FirstOrDefault(note => note.NotesId == NotesId);
            if (noteToUpdate != null)
            {
                noteToUpdate.Title = model.Title;
                noteToUpdate.Description = model.Description;
                noteToUpdate.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
                return noteToUpdate;
            }
            return null;
        }
        public NotesEntity Trash(int NotesId)
        {
            var trash = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (trash != null)
            {
                if (trash.ISTrash)
                {
                    trash.ISTrash = false;
                    context.SaveChanges();

                }
                else
                {
                    trash.ISTrash = true;
                }
            }
            return trash;
        }

        public NotesEntity DeleteNoteOperation(int NotesId, int id)
        {
            var deleted = context.NotesTable.FirstOrDefault(o => (o.NotesId == NotesId && o.UserId == id));
            if (deleted != null)
            {
                context.NotesTable.Remove(deleted);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid User Input");
            }
            return deleted;
        }
    }
}

