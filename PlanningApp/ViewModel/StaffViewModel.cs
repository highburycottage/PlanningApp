using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanningApp.Models;
using System.Web.Mvc;

namespace PlanningApp.ViewModel
{
    public class StaffViewModel
    {
        public project project { get; set; }
        public IEnumerable<SelectListItem> AllProjectStaff { get; set; }

        private List<int> _selectedconstructionStaff;
        public List<int> SelectedConstructionStaff
        {
            get
            {
                if (_selectedconstructionStaff == null)
                {
                    _selectedconstructionStaff = project.constructionStaffs.Select(m => m.staffID).ToList();
                }
                return _selectedconstructionStaff;
            }
            set { _selectedconstructionStaff = value; }
        }
    }
}
