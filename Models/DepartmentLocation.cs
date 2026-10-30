using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeesCh12.Models
{
    public class DepartmentLocation
    {
        //Composite Key and foreign key
        [DisplayName("Department ID")]
        public int DepartmentID { get; set; }

        //Composite Key and foreign key
        [DisplayName("Location ID")]
        public int LocationID { get; set; }

        //Navigation Property
        public Department? Department { get; set; }
        public Location Location { get; set; }
    }
}