using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class BlackList
    {
        public long IdBlackListCandidate { get; set; }
        public long? IdCandidate { get; set; }
        public int? IdUserCreated { get; set; }
        public string? Reason { get; set; }
        public DateTime? DatetimeCreated { get; set; }

        public virtual Candidate? IdCandidateNavigation { get; set; }
        public virtual VacancyUser? IdUserCreatedNavigation { get; set; }
    }
}
