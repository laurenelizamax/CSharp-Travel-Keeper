﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTravelKeeper.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        public string TripTitle { get; set; }


        [Required]
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        public bool IsFavorite { get; set; }
        public string Notes { get; set; }

        public string Photos { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<City> Cities { get; set; }

        public List<Traveler> Travelers { get; set; }

    }
}