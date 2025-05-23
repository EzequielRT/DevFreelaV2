using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.Input;

public class CreateSkillInputModel
{
    public string Description { get; set; }

    public Skill ToEntity()
        => new(Description);
}
