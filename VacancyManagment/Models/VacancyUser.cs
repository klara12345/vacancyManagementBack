using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class VacancyUser
    {
        public VacancyUser()
        {
            BlackLists = new HashSet<BlackList>();
            CandidateIdUserCreatedNavigations = new HashSet<Candidate>();
            CandidateIdUserLastSavedNavigations = new HashSet<Candidate>();
            VacancyCandidateIdUserReferedNavigations = new HashSet<VacancyCandidate>();
            VacancyCandidateIdUserValidateNavigations = new HashSet<VacancyCandidate>();
            VacancyIdTeamLeadNavigations = new HashSet<Vacancy>();
            VacancyIdUserCreatedNavigations = new HashSet<Vacancy>();
            VacancyIdUserSavedNavigations = new HashSet<Vacancy>();
        }

        public int IdUser { get; set; }
        public string User { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Email { get; set; }
        public int IdRole { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public string? Comment { get; set; }
        public string Salt { get; set; } = null!;

        public virtual VacancyUserRole IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<BlackList> BlackLists { get; set; }
        public virtual ICollection<Candidate> CandidateIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Candidate> CandidateIdUserLastSavedNavigations { get; set; }
        public virtual ICollection<VacancyCandidate> VacancyCandidateIdUserReferedNavigations { get; set; }
        public virtual ICollection<VacancyCandidate> VacancyCandidateIdUserValidateNavigations { get; set; }
        public virtual ICollection<Vacancy> VacancyIdTeamLeadNavigations { get; set; }
        public virtual ICollection<Vacancy> VacancyIdUserCreatedNavigations { get; set; }
        public virtual ICollection<Vacancy> VacancyIdUserSavedNavigations { get; set; }
    }
}
