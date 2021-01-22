using System.ComponentModel.DataAnnotations;

namespace MyCompanyAPI.DTOs
{
    public class DepartmentDTO
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
