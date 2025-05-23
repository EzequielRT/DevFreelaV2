namespace DevFreela.Core.Entities;

public class UserSkill : BaseEntity
{
    // EF Constructor
    protected UserSkill() { }

    public UserSkill(long userId, long skillId)
    {
        UserId = userId;
        SkillId = skillId;
    }

    public long UserId { get; private set; }
    public User User { get; private set; }
    public long SkillId { get; private set; }
    public Skill Skill { get; private set; }
}
