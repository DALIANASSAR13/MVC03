
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagement.ViewModels
{
    public class DepartmentDetailsViewModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string DepartmentState { get; set; } = string.Empty;

        public List<SelectListItem> StudentsOver25 { get; set; }
            = new List<SelectListItem>();
    }
}
