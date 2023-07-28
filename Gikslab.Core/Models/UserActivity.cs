namespace Gikslab.Core.Models
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Users { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
