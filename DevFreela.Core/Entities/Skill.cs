namespace DevFreela.Core.Entities
{
    public class Skill : BaseEntity
    {
        // EF Constructor
        protected Skill() { }

        public Skill(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
        public List<UserSkill> UserSkills { get; private set; }
    }
}
