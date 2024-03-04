using CommonLayer.RequestModel.LabelModel;
using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class LabelManagers : ILabelManager
    {
        private readonly ILabelInterface LabelRepository;

        public LabelManagers(ILabelInterface repository)
        {
            this.LabelRepository = repository;
        }
        public LabelEntity CreateLabel(LabelModel model, int noteId)
        {
            return LabelRepository.CreateLabel(model, noteId);
        }

        public int GetNoteIdByName(string noteName)
        {
            return LabelRepository.GetNoteIdByName(noteName);
        }

        public List<LabelEntity> DisplayAllLabel()
        {
            return LabelRepository.DisplayAllLabel();
        }

        public LabelEntity AssignLabel(int labelId, int noteId)
        {
            return LabelRepository.AssignLabel(labelId, noteId);
        }
    }
}
