using VacancyManagment.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace VacancyManagment.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly VacancyCadidatesContext _context;
        public AuthRepository(VacancyCadidatesContext context)
        {
            _context = context;
        }
        public async Task<VacancyUser> Login(string email, string password)
        {
            var user = await _context.VacancyUsers.FirstOrDefaultAsync(x => x.Email == email); //Get user from database.
            if (user == null)
                return null; // User does not exist.
            //if (!VerifyPassword(password, user.Password,user.Salt))
            //   return null;

            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }

        public async Task<VacancyUser> Register(VacancyUser user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);


                user.Password = Convert.ToBase64String(passwordHash);
                user.Salt = Convert.ToBase64String(passwordSalt);

                user.CreationDate = DateTime.Now;
                user.LastLoginDate=DateTime.Now;
                user.LastPasswordChangedDate = DateTime.Now;


            await _context.VacancyUsers.AddAsync(user); // Adding the user to context of users.
            await _context.SaveChangesAsync(); // Save changes to database.
            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
       

        public async Task<bool> UserExists(string email)
        {
            if (await _context.VacancyUsers.AnyAsync(x => x.Email == email))
                return true;
            return false;
        }
        
    }
}
