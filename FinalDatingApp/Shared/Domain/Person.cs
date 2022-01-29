using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDatingApp.Shared.Domain
{
    public class Person: BaseDomainModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int PreferenceId { get; set; }
        public virtual Preference Preference { get; set; }

        public ICollection<Match> FirstMatch { get; set; } = new List<Match>();
        public ICollection<Match> SecondMatch { get; set; } = new List<Match>();
    }
}
