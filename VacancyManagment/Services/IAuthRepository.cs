using VacancyManagment.Models;

namespace VacancyManagment.Services
{
    public interface IAuthRepository
    {   
        Task<VacancyUser> Register(VacancyUser user, string password);
        Task<VacancyUser> Login(string email, string pasword);
        Task<bool> UserExists(string email);
    }
}
