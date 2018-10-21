namespace YoureAProgrammer.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Answers
    {
        [Key]
        public int AnswersId { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageFullPath { get; set; }

        public int QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public virtual Questions Questions { get; set; }

       

        //public user UserID { get; set; }
    }
}
