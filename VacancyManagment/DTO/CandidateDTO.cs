using VacancyManagment.Models;

namespace VacancyManagment.DTO
{
    public class CandidateDTO
    {

        public string? CandidateName { get; set; }
        public string? CandidateSurname { get; set; }
        public string? CandidateMobile { get; set; }
        public string? CandidateEmail { get; set; }
        public string? CandidateCv { get; set; }

        
        //public CandidateDTO(Candidate candidate) =>
        //    (CandidateName, CandidateSurname, CandidateMobile, CandidateEmail, CandidateCv) =
        //    (candidate.CandidateName, candidate.CandidateSurname, candidate.CandidateMobile, candidate.CandidateEmail, candidate.CandidateCv);

    }
}
