using DevFreela.API.Entities;

namespace DevFreela.API.Models.View
{
    public class SkillItemViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public static SkillItemViewModel FromEntity(Skill skill)
        {
            return new()
            {
                Id = skill.Id,
                Description = skill.Description
            };
        }
    }
}
