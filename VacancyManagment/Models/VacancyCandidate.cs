using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class VacancyCandidate
    {
        public long IdVacancyCanddate { get; set; }
        public long? IdVacancy { get; set; }
        public long? IdCandidate { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsValid { get; set; }
        public int? IdUserValidate { get; set; }
        public DateTime? DatetimeValidated { get; set; }
        public int? IdUserRefered { get; set; }

        public virtual Candidate? IdCandidateNavigation { get; set; }
        public virtual VacancyUser? IdUserReferedNavigation { get; set; }
        public virtual VacancyUser? IdUserValidateNavigation { get; set; }
        public virtual Vacancy? IdVacancyNavigation { get; set; }
    }
}
