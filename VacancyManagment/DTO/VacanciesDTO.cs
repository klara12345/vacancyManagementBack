using VacancyManagment.Models;

namespace VacancyManagment.DTO
{
    public class VacanciesDTO
    {
        public string? Vacancy1 { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateSaved { get; set; }

        public VacanciesDTO() { }
        public VacanciesDTO(Vacancy vacancyItem) =>
        (Vacancy1, Description, IsActive , DateCreated , DateSaved) = 
          (vacancyItem.Vacancy1, vacancyItem.Description, vacancyItem.IsActive, vacancyItem.DateSaved, vacancyItem.DateCreated);
    }
}
