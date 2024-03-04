using CommonLayer.RequestModel.LabelModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRepository : ILabelInterface
    {
        private readonly UserContext context;
        public LabelRepository(UserContext Notecontext)
        {
            this.context = Notecontext;
        }
        public int GetNoteIdByName(string noteName)
        {
            var note = context.NotesTable.FirstOrDefault(n => n.Title == noteName);
            return note != null ? note.NotesId : -1; // Return -1 if note not found
        }

        public LabelEntity CreateLabel(LabelModel model, int noteId)
        {
            if (model != null && !string.IsNullOrEmpty(model.LabelName))
            {
                // Create a new label entity
                LabelEntity labelEntity = new LabelEntity
                {
                    LabelName = model.LabelName
                };

                // Add the label to the database
                context.LabelTable.Add(labelEntity);
                context.SaveChanges();

                // Update the note with the provided NoteId to associate it with the new label
                var noteToUpdate = context.NotesTable.FirstOrDefault(n => n.NotesId == noteId);
                if (noteToUpdate != null)
                {
                    noteToUpdate.LabelId = labelEntity.LabelId;
                    context.SaveChanges();
                }
                return labelEntity;
            }
            else
            {
                return null;
            }
        }
    }
}
