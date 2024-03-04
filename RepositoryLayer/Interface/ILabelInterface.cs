﻿using CommonLayer.RequestModel.LabelModel;
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
    }
}