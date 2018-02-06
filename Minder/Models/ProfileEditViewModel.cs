using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Minder.Models
{
    public class ProfileEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public GenderEnum Gender { get; set; }

        [Required]
        public GenderInterestEnum GenderInterests { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Height (in centimeters)")]
        public int Height { get; set; }

        public EthnicityEnum Ethnicity { get; set; }
        public string City { get; set; }
        public EducationEnum Education { get; set; }
        public List<Interest> Interests { get; set; }
        public virtual Picture ProfilePicture { get; set; }
    }
}