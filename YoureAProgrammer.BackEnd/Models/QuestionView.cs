namespace YoureAProgrammer.BackEnd.Models
{
    using System.Web;
    using Common.Models;

    public class QuestionView: Questions
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
