using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Enitity
{
    public class NotesEntity
    {
        [Key]
        public int NotesId;
        public string Title;
        public string Description;
        public DateTime Reminder;
        public string Colour;
        public string Image;
        public bool IsArchive;
        public bool IsPin;
        public bool ISTrash;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;

        [ForeignKey("NotesUser")]
        public int UserID;
    }
}
