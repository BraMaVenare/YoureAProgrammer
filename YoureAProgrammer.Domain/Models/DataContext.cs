namespace YoureAProgrammer.Domain.Models
{
    using System.Data.Entity;
    using Common.Models;

    public class DataContext : DbContext
    {
        public DataContext() : base("YoureProgrammerConnection")
        {
            
        }
        public virtual DbSet<Skills> Skills { set; get; }
        public virtual DbSet<Questions> Questions { set; get; }
        public virtual DbSet<Reunions> Reunions { set; get; }
        public virtual DbSet<Answers> Answers { set; get; }
    }
}
