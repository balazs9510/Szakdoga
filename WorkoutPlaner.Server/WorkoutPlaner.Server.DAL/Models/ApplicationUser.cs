using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorkoutPlaner.Server.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace WorkoutPlaner.Server.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weigth { get; set; }
        public bool AutSync { get; set; }
        public DateTime? LastSucessfulSync { get; set; }
        public List<Workout> Workouts { get; set; }
        public List<DoneWorkout> DoneWorkouts { get; set; }
        public ApplicationUser()
        {
            Workouts = new List<Workout>();
            DoneWorkouts = new List<DoneWorkout>();
            Age = 0;
            Weigth = 0;
        }
    }
}
