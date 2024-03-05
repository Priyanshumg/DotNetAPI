using RepositoryLayer.Enitity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.Entity
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotesId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool ISTrash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        // Navigation property
        public UserEntity User { get; set; }

        [ForeignKey("Label")]
        public int? LabelId { get; set; } // Nullable foreign key
        public LabelEntity Label { get; set; }

    }
}
