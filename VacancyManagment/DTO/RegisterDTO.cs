namespace VacancyManagment.DTO
{
    public class RegisterDTO
    {
        public string User { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }
        public bool IsActive { get; set; }
    }
}
