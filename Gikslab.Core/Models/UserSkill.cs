namespace Gikslab.Core.Models
{
    public class UserSkill
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Users { get; set; }
        public int SkillId { get; set; }
        public Skill Skills { get; set; }
    }
}
