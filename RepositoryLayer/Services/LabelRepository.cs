using CommonLayer.RequestModel.LabelModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<LabelEntity> DisplayAllLabel()
        {
            LabelModel label = new LabelModel();
            return context.LabelTable.ToList();
        }

        public LabelEntity AssignLabel(int labelId, int noteId)
        {
            var labelToAssign = context.LabelTable.FirstOrDefault(label => label.LabelId == labelId);
            var noteToUpdate = context.NotesTable.FirstOrDefault(note => note.NotesId == noteId);
            if (labelToAssign != null && noteToUpdate != null)
            {
                noteToUpdate.LabelId = labelToAssign.LabelId;
                context.SaveChanges();
                return labelToAssign;
            }
            else
            {
                return null;
            }
        }
        public LabelEntity DeleteLabel(int labelId)
        {
            var labelToDelete = context.LabelTable.FirstOrDefault(l => l.LabelId == labelId);
            if (labelToDelete != null)
            {
                context.LabelTable.Remove(labelToDelete);
                context.SaveChanges();
                return labelToDelete;
            }
            else
            {
                return null;
            }
        }
        public LabelEntity UpdateLabel(int labelId, LabelModel model)
        {
            var labelToUpdate = context.LabelTable.FirstOrDefault(l => l.LabelId == labelId);
            if (labelToUpdate != null && model != null && !string.IsNullOrEmpty(model.LabelName))
            {
                labelToUpdate.LabelName = model.LabelName;
                context.SaveChanges();
                return labelToUpdate;
            }
            else
            {
                return null;
            }
        }
    }
}
