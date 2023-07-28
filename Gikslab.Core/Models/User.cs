using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Gikslab.Core.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
        [JsonIgnore]
        public ICollection<Skill> Skills { get; set; }
        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; }
    }
}
