using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserCreationDto
    {
        [Required(ErrorMessage = "Please Enter Username.")]
        [MaxLength(100, ErrorMessage = "Max length is 100 Characters")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public OrganizationDto OrganizationName { get; set; }
    }
}
