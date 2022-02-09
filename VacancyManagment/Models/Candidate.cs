using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            BlackLists = new HashSet<BlackList>();
            VacancyCandidates = new HashSet<VacancyCandidate>();
        }

        public long IdCandidate { get; set; }
        public string? CandidateName { get; set; }
        public string? CandidateSurname { get; set; }
        public string? CandidateMobile { get; set; }
        public string? CandidateEmail { get; set; }
        public int? IdUserCreated { get; set; }
        public int? IdUserLastSaved { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateSaved { get; set; }
        public string? CandidateSsn { get; set; }
        public byte[]? CandidateCv { get; set; }

        public virtual VacancyUser? IdUserCreatedNavigation { get; set; }
        public virtual VacancyUser? IdUserLastSavedNavigation { get; set; }
        public virtual ICollection<BlackList> BlackLists { get; set; }
        public virtual ICollection<VacancyCandidate> VacancyCandidates { get; set; }
    }
}
