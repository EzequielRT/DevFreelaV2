using DevFreela.API.Entities;

namespace DevFreela.API.Models.Input
{
    public class CreateSkillInputModel
    {
        public string Description { get; set; }

        public Skill ToEntity()
            => new(Description);
    }
}
