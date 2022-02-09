using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class VacancyUserRole
    {
        public VacancyUserRole()
        {
            VacancyRoleActions = new HashSet<VacancyRoleAction>();
            VacancyUsers = new HashSet<VacancyUser>();
        }

        public int IdRole { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? AllowPrintA5 { get; set; }

        public virtual ICollection<VacancyRoleAction> VacancyRoleActions { get; set; }
        public virtual ICollection<VacancyUser> VacancyUsers { get; set; }
    }
}
