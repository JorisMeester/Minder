using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Minder.Models
{
    public class Profile
    {
        public int Id { get; set; }

        // Deze property willen we niet in het edit form tonen, daarom scaffoldcolumn(false)
        [ScaffoldColumn(false)]
        public ApplicationUser User { get; set; }

        [Required]
        public string Nickname { get; set; }
        public GenderEnum Gender { get; set; }

        [Display(Name = "I'm interested in")]
        public GenderInterestEnum GenderInterests { get; set; }

        // zorgen dat alleen een datum gekozen wordt, geen tijd
        [DataType(DataType.Date)]
       
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Height (in centimeters)")]
        public int Height { get; set; }
        public EthnicityEnum Ethnicity { get; set; }

        public string City { get; set; }
        public EducationEnum Education { get; set; }

        // Deze property willen we niet in het edit form tonen, daarom scaffoldcolumn(false)
        [ScaffoldColumn(false)]
        public List<Interest> Interests { get; set; }

        [Display(Name = "Profile picture")]
        // virtual zorgt ervoor dat deze automatisch wordt geladen uit de DB
        public virtual Picture ProfilePicture { get; set; }

        // Deze property willen we niet in het edit form tonen, daarom scaffoldcolumn(false)
        [ScaffoldColumn(false)]
        public List<Picture> Pictures { get; set; }

        // Deze properties willen we niet in het edit form tonen, daarom scaffoldcolumn(false)
        [ScaffoldColumn(false)]
        public int? Openness { get; set; }
        [ScaffoldColumn(false)]
        public int? Conscientiousness { get; set; }
        [ScaffoldColumn(false)]
        public int? Extraversion { get; set; }
        [ScaffoldColumn(false)]
        public int? Agreeableness { get; set; }
        [ScaffoldColumn(false)]
        public int? Neuroticism { get; set; }
    }
}