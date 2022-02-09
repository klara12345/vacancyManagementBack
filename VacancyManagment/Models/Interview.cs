using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class Interview
    {
        public long IdInterview { get; set; }
        public long? IdVacancy { get; set; }
        public long? IdCandidate { get; set; }
        public int? IdTlparticipant { get; set; }
        public int? IdHrParticipant { get; set; }
        public int? IdUserCreated { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        public decimal? ResultFromTl { get; set; }
        public decimal? ResultFromHr { get; set; }
        public decimal? RequestedSalary { get; set; }
        public decimal? OfferedSalary { get; set; }
        public bool? OfferAcceptance { get; set; }
        public string? CandidateComments { get; set; }
        public DateTime? InterviewDateTime { get; set; }
        public string? InterviewComments { get; set; }

        public virtual Vacancy? IdCandidateNavigation { get; set; }
    }
}
