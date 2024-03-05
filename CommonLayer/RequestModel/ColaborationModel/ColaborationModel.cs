using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.RequestModel.ColaborationModel
{
    public class ColaborationModel
    {
        public int ColabId { get; set; }
        public int UserIdToAddColab { get; set; }
        public int NoteId { get; set; }
    }
}
