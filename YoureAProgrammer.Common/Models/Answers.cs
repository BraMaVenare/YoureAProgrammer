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

        public string ImagePath { get; set; }

        public int QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public virtual Questions Questions { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "NoImage";
                }

                return $"https://pratice1-2018-iiapi.azurewebsites.net/{this.ImagePath.Substring(1)}";
            }
        }

        //public user UserID { get; set; }
    }
}
