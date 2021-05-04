using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public class OrganizationCreation
    {
        [Required(ErrorMessage = "Please Enter Organization Name.")]
        [MaxLength(100, ErrorMessage = "Max length is 100 Characters")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public string OrgName { get; set; }

        [Required(ErrorMessage = "Please Enter City Name.")]
        [MaxLength(100, ErrorMessage = "Max length is 100 Characters")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter City Name.")]
        [MaxLength(100, ErrorMessage = "Max length is 100 Characters")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public string Country { get; set; }
    }
}
