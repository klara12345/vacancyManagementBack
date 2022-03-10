using VacancyManagment.Models;


namespace VacancyManagment.Services
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly VacancyCadidatesContext _context;


        public CandidateRepository(VacancyCadidatesContext context)
        {
            _context = context;

        }

        
        public async Task<Candidate> Upload(Candidate candidate, string candidateCv)
        {


            

            candidate.CandidateCv = Convert.FromBase64String(candidateCv);
            candidate.DateCreated = DateTime.Now;
            candidate.DateSaved = DateTime.Now;
            

            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }



    }
 }


