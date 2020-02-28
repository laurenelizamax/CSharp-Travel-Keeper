using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTravelKeeper.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "Activity")]
        public string ActivityName { get; set; }
        public string Description { get; set; }

        [Display(Name = "Activity Site")]
        public string ActivityWebsite { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
