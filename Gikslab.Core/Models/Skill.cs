using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gikslab.Core.Models
{
    public class Skill
    {
        [Column("SkillId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Skill's name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
