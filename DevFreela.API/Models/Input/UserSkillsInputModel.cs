namespace DevFreela.API.Models.Input
{
    public class UserSkillsInputModel
    {
        public UserSkillsInputModel(long userId, List<long> skillsIds)
        {
            UserId = userId;
            SkillsIds = skillsIds;
        }

        public long UserId { get; set; }
        public List<long> SkillsIds { get; set; }
    }
}
