namespace YoureAProgrammer.BackEnd.Models
{
    using System.Web;
    using Common.Models;

    public class AnswerView:Answers
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}