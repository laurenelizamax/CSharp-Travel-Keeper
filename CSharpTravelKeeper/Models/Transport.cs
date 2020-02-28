using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTravelKeeper.Models
{
    public class Transport
    {
        public int Id { get; set; }

        [Display(Name = "Transportation")]
        public string TransportTitle { get; set; }
        public string Notes { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
