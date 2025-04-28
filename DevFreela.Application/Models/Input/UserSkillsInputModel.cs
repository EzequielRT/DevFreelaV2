using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.Input
{
    public class UserSkillsInputModel
    {
        public long UserId { get; private set; }
        public List<long> SkillIds { get; set; }

        public void SetUserId(long id) => UserId = id;

        public List<UserSkill> ToEntities()
            => SkillIds
                .Select(skillId => new UserSkill(UserId, skillId))
                .ToList();
    }
}
