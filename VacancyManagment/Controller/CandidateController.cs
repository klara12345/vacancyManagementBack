using Microsoft.AspNetCore.Mvc;
using VacancyManagment.DTO;
using VacancyManagment.Models;
using VacancyManagment.Services;

namespace VacancyManagment.Controller
{
    [Route("api/Candidate")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository _repo;
        public CandidateController(ICandidateRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("upload")]

       
        public async Task<IActionResult> Upload([FromBody] CandidateDTO candidateData)
        {



            var candidateToCreate = new Candidate
            {

                CandidateName = candidateData.CandidateName,
                CandidateSurname = candidateData.CandidateSurname,
                CandidateMobile = candidateData.CandidateMobile,
                CandidateEmail = candidateData.CandidateEmail



            };

            var createCandidate = await _repo.Upload(candidateToCreate, candidateData.CandidateCv);
            return StatusCode(201);
            

        }
        


        


    }
}
