using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutPlaner.Server.DAL.Models;
using WorkoutPlaner.Server.Data;

namespace WorkoutPlaner.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/DoneWorkouts")]
    public class DoneWorkoutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoneWorkoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DoneWorkouts
        [HttpGet("{userId}")]
        public IEnumerable<DoneWorkout> GetDoneWorkouts([FromRoute]string userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            return user.DoneWorkouts;
        }

        //// GET: api/DoneWorkouts/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDoneWorkout([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var doneWorkout = await _context.DoneWorkouts.SingleOrDefaultAsync(m => m.Id == id);

        //    if (doneWorkout == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(doneWorkout);
        //}

        //// PUT: api/DoneWorkouts/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDoneWorkout([FromRoute] string id, [FromBody] DoneWorkout doneWorkout)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != doneWorkout.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(doneWorkout).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DoneWorkoutExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/DoneWorkouts
        [HttpPost("{userId}")]
        public async Task<IActionResult> PostDoneWorkout([FromRoute]string userId,
            [FromBody] DoneWorkout doneWorkout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<DoneExerciseItem> newDoneExerciseItems = new List<DoneExerciseItem>();
            foreach (var dEItem in doneWorkout.DoneExerciseItems)
            {
                var newDEItem = new DoneExerciseItem()
                {
                    CompleteTime = dEItem.CompleteTime,
                   // ExerciseItem = _context.ExerciseItems.SingleOrDefault(u => u.ClientId.Equals(dEItem.ClientId)),
                   
                    IsCompleted = dEItem.IsCompleted,
                };
                newDoneExerciseItems.Add(newDEItem);
            }
            var newDoneWorkout = new DoneWorkout()
            {

                CompleteTime = doneWorkout.CompleteTime,
                DoneExerciseItems = newDoneExerciseItems,
                Date = doneWorkout.Date,
                Rating = doneWorkout.Rating,
               // Workout = _context.Workouts.SingleOrDefault(w => w.ClientId.Equals(doneWorkout.Workout.ClientId)),
            };
            _context.DoneWorkouts.Add(newDoneWorkout);
            newDoneWorkout.ServerId = newDoneWorkout.Id;
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            user.DoneWorkouts.Add(newDoneWorkout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoneWorkout", new { id = newDoneWorkout.Id }, newDoneWorkout);
        }

        // DELETE: api/DoneWorkouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoneWorkout([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doneWorkout = await _context.DoneWorkouts.SingleOrDefaultAsync(m => m.Id == id);
            if (doneWorkout == null)
            {
                return NotFound();
            }

            _context.DoneWorkouts.Remove(doneWorkout);
            await _context.SaveChangesAsync();

            return Ok(doneWorkout);
        }

        private bool DoneWorkoutExists(string id)
        {
            return _context.DoneWorkouts.Any(e => e.Id == id);
        }
    }
}