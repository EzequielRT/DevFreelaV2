using DevFreela.API.Entities;

namespace DevFreela.API.Models.View
{
    public class SkillItemViewModel
    {
        public SkillItemViewModel(long id, string description)
        {
            Id = id;
            Description = description;
        }

        public long Id { get; private set; }
        public string Description { get; private set; }

        public static SkillItemViewModel FromEntity(Skill skill)
        {
            return new(skill.Id, skill.Description);
        }
    }
}
