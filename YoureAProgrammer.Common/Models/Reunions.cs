using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoureAProgrammer.Common.Models
{
    public class Reunions
    {
        [Key]
        public int ReunionId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string  Localizate { get; set; }

        [Required]
        public DateTime Date { get; set; }

        //public user UserID { get; set; }

        public int SkillId { get; set; }
        [ForeignKey("SkillId")]
        public virtual Skills Skills { get; set; }
    }
}
