using System.ComponentModel.DataAnnotations.Schema;

namespace Gikslab.Core.Models
{
    public class Activity
    {
        [Column("ActivityId")]
        public int Id { get; set; }

        [ForeignKey(nameof(Skill))]
        public int SkillId { get; set; }
        public Skill Skills { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
