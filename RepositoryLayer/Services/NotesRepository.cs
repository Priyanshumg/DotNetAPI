using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CommonLayer.RequestModel.NotesModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
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
        public NotesEntity CreateNote(CreateNotes model, int UserId)
        {
            NotesEntity Note = new NotesEntity();
            Note.UserId = UserId;
            Note.Title = model.Title;
            Note.Description = model.Description;
            Note.Colour = model.Colour;
            Note.Image = model.Image;
            Note.IsArchive = model.IsArchive;
            Note.IsPin = model.IsPin;
            Note.ISTrash = model.IsTrash;
            Note.CreatedAt = DateTime.UtcNow;
            Note.UpdatedAt = DateTime.UtcNow;
            context.NotesTable.Add(Note);
            context.SaveChanges();
            return Note;
        }
        public List<NotesEntity> GetAllNote(int Userid)
        {
            return context.NotesTable.Where<NotesEntity>(AllUser => AllUser.UserId == Userid).ToList();
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

        public NotesEntity DeleteNoteOperation(int NotesId)
        {
            var deleted = context.NotesTable.FirstOrDefault(person => person.NotesId == NotesId);
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
        public NotesEntity Archive(int NotesId)
        {
            var archive = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (archive != null)
            {
                if (archive.IsArchive)
                {
                    archive.IsArchive = false;
                    context.SaveChanges();
                }
                else
                {
                    archive.IsArchive = true;
                }
                return archive;
            }
            else
            {
                throw new Exception("IsArchive not found");
            }
        }
        public NotesEntity Colour(int NotesId)
        {
            var color = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (color != null)
            {
                //color.Colour = "Blue";
                color.Colour = "Green";
                context.SaveChanges();
            }
            return color;

        }
        public NotesEntity Reminder(int NotesId)
        {
            var remind = context.NotesTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (remind != null)
            {
                remind.Reminder = DateTime.UtcNow;
                context.SaveChanges();
            }
            return remind;
        }

        //Image
        public string UploadImage(string filepath, int NotesId, int UserId)
        {
            try
            {
                var filter = context.NotesTable.Where(User => User.UserId == UserId);
                if (filter != null)
                {
                    var findNotes = filter.FirstOrDefault(Note => Note.NotesId == NotesId);
                    if (findNotes != null)
                    {
                        Account account = new Account("dp4bw10zf", "296469352489476", "***************************");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filepath),
                            PublicId = findNotes.Title
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                        findNotes.UpdatedAt = DateTime.Now;
                        findNotes.Image = uploadResult.Url.ToString();
                        context.SaveChanges();
                        return "Upload Successfull";
                    }
                    return null;
                }
                else { return null; }
            }
            catch (Exception ex) { return null; }
        }
    }
    public class Label : ILabelNotesRepository
    {
        private readonly UserContext context;
        public Label(UserContext context)
        {
            this.context = context;
        }
    }
}

