using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class VacancyRoleAction
    {
        public int IdRoleAction { get; set; }
        public int IdRole { get; set; }
        public int IdAction { get; set; }
        public bool IsActive { get; set; }

        public virtual VacancyAction IdActionNavigation { get; set; } = null!;
        public virtual VacancyUserRole IdRoleNavigation { get; set; } = null!;
    }
}
