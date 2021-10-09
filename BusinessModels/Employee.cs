using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessModels
{
    public class Employee
    {
        private int id;
        private string firstName;
        private string middleName;
        private string lastName;

        public Employee()
        {
            id = 0;
            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
        }

        [Display(Name = "ID")]
        public int ID { get => id; set => id = value; }

        [Required(ErrorMessage = "First Name cannot be empty")]
        [Display(Name = "First Name")]
        [MinLength(2)]
        public string FirstName { get => firstName; set => firstName = value; }

        [Required(ErrorMessage = "Middle Name cannot be empty")]
        [Display(Name = "Middle Name")]
        [MinLength(2)]
        public string MiddleName { get => middleName; set => middleName = value; }

        [Required(ErrorMessage = "Last Name cannot be empty")]
        [Display(Name = "Last Name")]
        [MinLength(2)]
        public string LastName { get => lastName; set => lastName = value; }
    }
}
