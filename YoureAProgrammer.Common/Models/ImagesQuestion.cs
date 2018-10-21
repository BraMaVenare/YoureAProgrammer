
namespace YoureAProgrammer.Common.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ImagesQuestion
    {
        [Key]
        public int ImageQuestionID { get; set; }

        [Required]
        public string ImageFullPath { get; set; }

        public int QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public virtual Questions Questions { get; set; }

        //public user UserID { get; set; }
    }
}
