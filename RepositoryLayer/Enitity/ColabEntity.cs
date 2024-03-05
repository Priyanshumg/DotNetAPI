using Microsoft.AspNetCore.SignalR;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Enitity
{
    public class ColabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColabId { get; set; }

        // Foreign key referencing the UserEntity
        [ForeignKey("User")]
        public int UserIdToAddColab { get; set; }

        // Foreign key referencing the NoteEntity
        [ForeignKey("Note")]
        public int NoteId { get; set; }

        // Navigation properties
        public UserEntity User { get; set; }
        public NotesEntity Note { get; set; }
    }
}
