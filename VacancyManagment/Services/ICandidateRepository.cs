using VacancyManagment.Models;

namespace VacancyManagment.Services
{
    public interface ICandidateRepository
    {

        Task<Candidate> Upload(Candidate candidate, string candidateCv);

        
        
    }

}
