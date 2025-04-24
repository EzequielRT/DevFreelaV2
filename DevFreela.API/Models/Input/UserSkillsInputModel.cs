using DevFreela.API.Entities;

namespace DevFreela.API.Models.Input
{
    public class UserSkillsInputModel
    {
        public long UserId { get; private set; }
        public List<long> SkillIds { get; set; }

        public void SetUserId(int id) => UserId = id;

        public List<UserSkill> ToEntities()
            => SkillIds
                .Select(skillId => new UserSkill(UserId, skillId))
                .ToList();
    }
}
