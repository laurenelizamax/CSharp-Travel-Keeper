using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTravelKeeper.Models
{
    public class Traveler
    {
        public int Id { get; set; }

        [Display(Name = "Traveler")]
        public string FellowTraveler { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Website")]
        public string TravelerWebsite { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
