using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Gikslab.Core.Models;

namespace Gikslab.Repository.Data
{
    public class SkillData : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasData(
                new Skill
                {
                    Id = 1,
                    Name = "Laravel"
                },

                new Skill
                {
                    Id = 2,
                    Name = ".Net"
                },

                new Skill
                {
                    Id = 3,
                    Name = "Golang"
                },

                new Skill
                {
                    Id = 4,
                    Name = "Node Js"
                },

                new Skill
                {
                    Id = 5,
                    Name = "Java"
                },

                new Skill
                {
                    Id = 6,
                    Name = "Codeigniter"
                });
        }
    }
}