using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel.NotesModel
{
    public class CreateNotes
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int LabelID { get; set; }
    }
}
