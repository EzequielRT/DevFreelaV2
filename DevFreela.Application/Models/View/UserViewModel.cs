﻿using DevFreela.Core.Entities;

namespace DevFreela.Application.Models.View
{
    public class UserViewModel
    {
        public UserViewModel(string fullName, string email, DateTime birthDate, List<string> skills)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<string> Skills { get; private set; }

        public static UserViewModel FromEntity(User user)
        {
            return new(
                user.FullName,
                user.Email,
                user.BirthDate,
                user.Skills.Select(s => s.Skill.Description).ToList());
        }
    }
}
