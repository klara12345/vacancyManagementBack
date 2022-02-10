using VacancyManagment.Models;

namespace VacancyManagment.DTO
{
    public class CandidateDTO
    {
        public string? CandidateName { get; set; }
        public string? CandidateSurname { get; set; }
        public string? CandidateMobile { get; set; }
        public string? CandidateEmail { get; set; }
        public byte[]? CandidateCv { get; set; }
        public CandidateDTO() { }
        public CandidateDTO(Candidate candidateItem) =>
            (CandidateName, CandidateSurname, CandidateMobile, CandidateEmail, CandidateCv) =
            (candidateItem.CandidateName, candidateItem.CandidateSurname, candidateItem.CandidateMobile, candidateItem.CandidateEmail, candidateItem.CandidateCv);
       

    }
}
