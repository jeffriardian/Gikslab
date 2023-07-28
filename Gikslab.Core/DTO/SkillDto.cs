using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gikslab.Core.DTO
{
    public class SkillDto
    {
        [Column("SkillId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Skill's name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }
    }
}
