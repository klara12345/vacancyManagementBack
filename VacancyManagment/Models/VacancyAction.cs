using System;
using System.Collections.Generic;

namespace VacancyManagment.Models
{
    public partial class VacancyAction
    {
        public VacancyAction()
        {
            VacancyRoleActions = new HashSet<VacancyRoleAction>();
        }

        public int IdAction { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? CanLog { get; set; }
        public string? LogMsg { get; set; }

        public virtual ICollection<VacancyRoleAction> VacancyRoleActions { get; set; }
    }
}
