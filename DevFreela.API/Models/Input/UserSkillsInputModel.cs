namespace DevFreela.API.Models.Input
{
    public class UserSkillsInputModel
    {
        public UserSkillsInputModel(long userId, List<long> skillIds)
        {
            UserId = userId;
            SkillIds = skillIds;
        }

        public long UserId { get; set; }
        public List<long> SkillIds { get; set; }
    }
}
