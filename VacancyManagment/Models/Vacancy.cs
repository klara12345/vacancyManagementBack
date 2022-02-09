using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class Vacancy
    {
        public Vacancy()
        {
            Interviews = new HashSet<Interview>();
            VacancyCandidates = new HashSet<VacancyCandidate>();
        }

        public long IdVacancy { get; set; }
        public string? Vacancy1 { get; set; }
        public string? Description { get; set; }
        public int? IdUserCreated { get; set; }
        public int? IdTeamLead { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateSaved { get; set; }
        public int? IdUserSaved { get; set; }

        public virtual VacancyUser? IdTeamLeadNavigation { get; set; }
        public virtual VacancyUser? IdUserCreatedNavigation { get; set; }
        public virtual VacancyUser? IdUserSavedNavigation { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<VacancyCandidate> VacancyCandidates { get; set; }
    }
}
