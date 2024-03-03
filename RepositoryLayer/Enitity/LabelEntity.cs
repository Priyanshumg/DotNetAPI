using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.Enitity
{
    public class LabelEntity
    {
        [Key]
        public int LabelID {  get; set; }

        public int LabelName {  get; set; }

        [ForeignKey("NotesEntity")]
        public int NotesId { get; set; }
    }
}
