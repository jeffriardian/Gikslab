using System.ComponentModel.DataAnnotations;

namespace Gikslab.Core.DTO
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
    }

    public class ActivityCreationDto : ActivityAddUpdateDto
    {
        [Required(ErrorMessage = "Choose atleast one participant.")]
        public ICollection<GetUserDto>? Participants { get; set; }
    }

    public class ActivityUpdateDto : ActivityAddUpdateDto
    {
    }


    public abstract class ActivityAddUpdateDto
    {
        [Required(ErrorMessage = "Activity name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Title Name is 50 characters.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Description is 150 characters.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Start Date is a required field.")]
        public DateTime Startdate { get; set; }
        [Required(ErrorMessage = "End Date is a required field.")]
        public DateTime Enddate { get; set; }
        [Required(ErrorMessage = "Skill name is a required field.")]
        public string SkillName { get; set; }
    }
}
