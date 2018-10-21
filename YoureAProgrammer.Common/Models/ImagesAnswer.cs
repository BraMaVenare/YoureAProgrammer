using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoureAProgrammer.Common.Models
{
    public class ImagesAnswer
    {
        [Key]
        public int ImageAnswerId { get; set; }

        [Required]
        public string ImageFullPath { get; set; }

        public int AnswersId { get; set; }
        [ForeignKey("AnswersId")]
        public virtual Answers Answers { get; set; }

        //public user UserID { get; set; }
    }
}
