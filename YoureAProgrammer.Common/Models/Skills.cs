namespace YoureAProgrammer.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Skills
    {
        [Key]
        public int SkillId { get; set; }

        [StringLength(150), Required]
        public string Name { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }

        public virtual ICollection<Reunions> Reunions { get; set; }

    }
}
