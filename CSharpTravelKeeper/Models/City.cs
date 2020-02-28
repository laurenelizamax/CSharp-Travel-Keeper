using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTravelKeeper.Models
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<ActivityEvent> ActivityEvents { get; set; }

        public List<Lodging> Lodgings { get; set; }

        public List<Transport> Transports { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }

    }
}
