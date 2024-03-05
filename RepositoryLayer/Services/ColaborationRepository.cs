using CommonLayer.RequestModel.ColaborationModel;
using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using RepositoryLayer.Services;
using CommonLayer.RequestModel.NotesModel;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class ColaborationRepository : IColaborationInterface
    {
        private readonly UserContext context;


        public ColaborationRepository(UserContext context, ColaborationModel model)
        {
            this.context = context;
        }

        public ColabEntity AddColaboratory(int userIdToColab, int NoteIdToColab)
        {
            ColabEntity model = new ColabEntity
            {
                UserIdToAddColab = userIdToColab,
                NoteId = NoteIdToColab
            };

            addNote(NoteIdToColab, userIdToColab);

            context.ColabTable.Add(model);
            context.SaveChanges();
            return model;
        }

        public NotesEntity addNote(int noteIdToColab, int userIdToColab)
        {
            // Find the original note in the database
            NotesEntity originalNote = context.NotesTable.Find(noteIdToColab);

            if (originalNote != null)
            {
                // Create a new note for the collaborator
                NotesEntity collaboratorNote = new NotesEntity
                {
                    Title = originalNote.Title,
                    Description = originalNote.Description,
                    Colour = originalNote.Colour,
                    Image = originalNote.Image,
                    IsArchive = originalNote.IsArchive,
                    IsPin = originalNote.IsPin,
                    ISTrash = originalNote.ISTrash,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    // Set the UserId to the collaborator's ID
                    UserId = userIdToColab
                };

                // Add the collaborator's note to the database
                context.NotesTable.Add(collaboratorNote);

                // Save changes to the database
                context.SaveChanges();

                // Return the collaborator's note
                return collaboratorNote;
            }

            // Return null if the original note was not found
            return null;
        }
        public ColabEntity RemoveColaborator(int NoteIdToRemoveFromColab, int UserIdToRemoveFromColab)
        {
            var colabToRemove = context.ColabTable
                .Where(
                c => c.NoteId == NoteIdToRemoveFromColab 
                && 
                c.UserIdToAddColab == UserIdToRemoveFromColab).FirstOrDefault();
            var notesToRemove = context.NotesTable.Where(n => n.UserId == UserIdToRemoveFromColab).FirstOrDefault();
            if (colabToRemove != null && notesToRemove != null)
            {
                context.Remove(colabToRemove);
                context.NotesTable.Remove(notesToRemove);
                context.SaveChanges();
                return colabToRemove;
            }
            else
            {
                return null;
            }
        }
    }
}
