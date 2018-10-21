namespace YoureAProgrammer.BackEnd.Models
{
    using Domain.Models;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<YoureAProgrammer.Common.Models.Skills> Skills { get; set; }
    }
}