using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlaner.Server.DAL.Models;
using WorkoutPlaner.Server.Data;
using WorkoutPlaner.Server.Controllers.Base;
using Microsoft.AspNetCore.Identity;
using WorkoutPlaner.Server.Models;
using Microsoft.AspNetCore.Authorization;
using WorkoutPlaner.Server.DAL.Enumerations;

namespace WorkoutPlaner.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/Workouts")]
    public class WorkoutsController : BaseController
    {
        public WorkoutsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> s,
            IHttpContextAccessor accessor)
            :base(userManager,accessor,context,s)
        {
        }

        // GET: api/Workouts
        [HttpGet]
        public IEnumerable<Workout> GetWorkouts()
        { 
            return _context.Workouts
                .Include(w => w.ExerciseItems)
                    .ThenInclude(e => e.Exercise);
        }

        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkout([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _context.Workouts.SingleOrDefaultAsync(m => m.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        // PUT: api/Workouts/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutWorkout([FromRoute] string userId, 
            [FromBody] Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var eWorkout = _context.Workouts
                .Include(w=> w.ExerciseItems)
                .SingleOrDefault(w => w.Id.Equals(workout.ServerId));
            if (eWorkout == null)
                return NoContent();
            foreach (var cExerciseItem in workout.ExerciseItems)
            {
                var sExerciseItem = _context.ExerciseItems
                    .SingleOrDefault(e => e.Id.Equals(cExerciseItem.ServerId));
                if (cExerciseItem.ServerId == null)
                {
                    sExerciseItem = new ExerciseItem()
                    {
                        Exercise = _context.Exercises
                            .SingleOrDefault(e => cExerciseItem.Exercise.Name.Equals(e.Name)),
                        Reps = cExerciseItem.Reps,
                        Serial = cExerciseItem.Serial,
                        Workout = eWorkout
                    };
                    eWorkout.ExerciseItems.Add(sExerciseItem);
                }
                if(cExerciseItem.State == StoreItemState.UpdatedFromClientSide)
                {
                    sExerciseItem.Reps = cExerciseItem.Reps;
                    sExerciseItem.Serial = cExerciseItem.Serial;
                    _context.ExerciseItems.Update(sExerciseItem);
                }
                if(cExerciseItem.State == StoreItemState.DeletedFromClientSide)
                {
                    eWorkout.ExerciseItems.Remove(sExerciseItem);
                    _context.ExerciseItems.Remove(sExerciseItem);
                }
            }

            _context.Workouts.Update(eWorkout);
            await _context.SaveChangesAsync();
            if(workout.State == StoreItemState.UpdatedFromClientSide)
            {
                eWorkout.Name = workout.Name;
                eWorkout.Difficulty = workout.Difficulty;
            }
            foreach (var item in eWorkout.ExerciseItems)
            {
                item.ServerId = item.Id;
            }
            try
            {
                _context.Workouts.Update(eWorkout);
                _context.SaveChanges();
                return Created("api/Workouts/",eWorkout);
            }
            catch
            {
                return NoContent();
            }
        }

        // POST: api/Workouts
        [HttpPost("{userId}")]
        public async Task<IActionResult> PostWorkout([FromRoute]string userId,
            [FromBody] Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<ExerciseItem> newExerciseItems = new List<ExerciseItem>();
            foreach (var exerciseItem in workout.ExerciseItems)
            {
                ExerciseItem newExerciseItem = new ExerciseItem()
                {
                    Exercise = _context.Exercises
                    .SingleOrDefault(e => exerciseItem.Exercise.Name.Equals(e.Name)),
                    Reps = exerciseItem.Reps,
                    Serial = exerciseItem.Serial,
                };
                newExerciseItems.Add(newExerciseItem);
            }
            Workout newItem = new Workout()
            {
                Difficulty = workout.Difficulty,
                ExerciseItems = newExerciseItems,
                Name = workout.Name,
            };
            
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (user == null)
                return BadRequest();
            user.Workouts.Add(newItem);

            _context.Workouts.Add(newItem);
            newItem.ServerId = newItem.Id;
            foreach (var item in newItem.ExerciseItems)
            {
                item.ServerId = item.Id;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkout", new { id = newItem.Id }, newItem);
        }

        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _context.Workouts
                .Include(w => w.ExerciseItems)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return Ok(workout);
        }

        private bool WorkoutExists(string id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }
    }
}