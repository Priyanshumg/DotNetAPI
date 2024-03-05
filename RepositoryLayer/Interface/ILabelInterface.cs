using CommonLayer.RequestModel.LabelModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelInterface
    {
        public LabelEntity CreateLabel(LabelModel model, int noteId);
        public int GetNoteIdByName(string noteName);
        public List<LabelEntity> DisplayAllLabel();
        public LabelEntity AssignLabel(int labelId, int noteId);
        public LabelEntity DeleteLabel(int labelId);

        public LabelEntity UpdateLabel(int labelId, LabelModel model);
    }
}
