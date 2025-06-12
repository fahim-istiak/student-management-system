using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System__MVC_.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(5, 100, ErrorMessage = "Age must be between 5 and 100")]
        public int Age { get; set; }

        [Display(Name = "Profile Picture")]
        [StringLength(255, ErrorMessage = "Image path cannot be longer than 255 characters")]
        public string ImagePath { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
} 