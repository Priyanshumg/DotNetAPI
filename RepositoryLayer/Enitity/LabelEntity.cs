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
        public int LabelId {  get; set; }

        public string LabelName {  get; set; }
    }
}
