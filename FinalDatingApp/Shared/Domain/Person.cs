using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Person: BaseDomainModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "First Name does not meet length requirements")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Last Name does not meet length requirements")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(18, 99, ErrorMessage = "Value for age must be between {1} and {2}.")]
        public int Age { get; set; }

        [Required]
        public int PreferenceId { get; set; }
        public virtual Preference Preference { get; set; }

        public ICollection<Match> FirstMatch { get; set; } = new List<Match>();
        public ICollection<Match> SecondMatch { get; set; } = new List<Match>();
    }
}
