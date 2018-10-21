namespace YoureAProgrammer.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }

        [StringLength(50), Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageFullPath { get; set; }

        //public user UserID { get; set; }

        public int SkillId { get; set; }
        [ForeignKey("SkillId")]
        public virtual Skills Skills { get; set; }


        public virtual ICollection<Answers> Answers { get; set; }
    }
}
