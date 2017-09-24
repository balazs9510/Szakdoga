using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.Common;
using WorkoutPlaner.Common.Enumerations;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using Xamarin.Forms;

namespace WorkoutPlaner.Sync
{
    public class SyncManager
    {
        private static SyncManager instance = null;
        private readonly ApplicationContext _context;
        private string _userId = string.Empty;
        public List<SyncMessage> SyncMessages { get; set; }
        public NetworkService NetworkService { get; set; }
        public SyncManager(ApplicationContext context, string userId, NetworkService ns)
        {
            _context = context;
            _userId = userId;
            NetworkService = ns;
            SyncMessages = new List<SyncMessage>();
        }

        public async void SyncAsync()
        {
            
            SyncMessages.Add(await UploadUserWorkoutsAsync(NetworkService));
            SyncMessages.Add(await UploadUserDoneWorkoutsAsync(NetworkService));
            SyncMessages.Add(await DownloadUserWorkoutsAsync(NetworkService));
            MessagingCenter.Send(this, MessagingKeys.SyncDone, SyncMessages);
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(_userId));
            user.LastSucessfulSync = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        private async Task<SyncMessage> UploadUserWorkoutsAsync(NetworkService nS)
        {
            var workouts = _context.Workouts
                .Include(w => w.ExerciseItems)
                    .ThenInclude(e => e.Exercise)
                .Where(w => w.UserId.Equals(_userId));
            foreach (var workout in workouts)
            {
                //Just created entities.
                if (workout.State == StoreItemState.CreatedClientside)
                {
                    var response = await nS.PostWorkout(_userId, workout);
                    if (response.IsSuccessStatusCode)
                    {
                        Workout sWorkout = await nS.GetObjectFromResponseAsync<Workout>(response);
                        workout.ServerId = sWorkout.ServerId;
                        workout.ExerciseItems.RemoveAll(e => true);
                        workout.State = StoreItemState.EqualsWithServer;
                        foreach (var item in sWorkout.ExerciseItems)
                        {
                            var newExerciseItem = new ExerciseItem
                            {
                                Exercise = _context.Exercises.SingleOrDefault(e => e.Name.Equals(item.Exercise.Name)),
                                Reps = item.Reps,
                                State = StoreItemState.EqualsWithServer,
                                ServerId = item.Id,
                                Serial = item.Serial
                            };
                            workout.ExerciseItems.Add(newExerciseItem);
                        }
                    }
                    else
                        return new SyncMessage() { Result = SyncronizationMessages.NewInstancesUploadFail };
                }
                //Entities that had been modified.
                if (workout.State == StoreItemState.UpdatedFromClientSide)
                {
                    //If the serverId is null than this entity hasn't post to the server yet.
                    if (workout.ServerId == null)
                    {
                        var response = await nS.PostWorkout(_userId, workout);
                        if (response.IsSuccessStatusCode)
                        {
                            Workout sWorkout = await nS.GetObjectFromResponseAsync<Workout>(response);
                            workout.ServerId = sWorkout.ServerId;
                            workout.ExerciseItems.RemoveAll(e => true);
                            workout.State = StoreItemState.EqualsWithServer;
                            foreach (var item in sWorkout.ExerciseItems)
                            {
                                var newExerciseItem = new ExerciseItem
                                {
                                    Exercise = _context.Exercises.SingleOrDefault(e => e.Name.Equals(item.Exercise.Name)),
                                    Reps = item.Reps,
                                    State = StoreItemState.EqualsWithServer,
                                    ServerId = item.Id,
                                    Serial = item.Serial
                                };
                                workout.ExerciseItems.Add(newExerciseItem);
                            }
                        }
                        else
                            return new SyncMessage() { Result = SyncronizationMessages.NewInstancesUploadFail };
                    }
                    else
                    {
                        var response = await nS.PutWorkout(_userId, workout);
                        if (response.IsSuccessStatusCode)
                        {
                            Workout sWorkout = await nS.GetObjectFromResponseAsync<Workout>(response);
                            workout.State = StoreItemState.EqualsWithServer;
                            workout.ExerciseItems.RemoveAll(e => true);
                            foreach (var item in sWorkout.ExerciseItems)
                            {
                                var newExerciseItem = new ExerciseItem
                                {
                                    Exercise = _context.Exercises.SingleOrDefault(e => e.Name.Equals(item.Exercise.Name)),
                                    Reps = item.Reps,
                                    State = StoreItemState.EqualsWithServer,
                                    ServerId = item.Id,
                                    Serial = item.Serial
                                };
                                workout.ExerciseItems.Add(newExerciseItem);
                            }
                        }
                        else
                            return new SyncMessage() { Result = SyncronizationMessages.NewInstancesUploadFail };
                    }
                }
            }
            _context.Workouts.UpdateRange(workouts);
            _context.SaveChanges();
            return new SyncMessage()
            {
                IsSucessfull = true,
                Result = SyncronizationMessages.NewInstancesUploadSucess
            };
        }
        private async Task<SyncMessage> UploadUserDoneWorkoutsAsync(NetworkService nS)
        {
            var doneWorkouts = _context.DoneWorkouts
                .Include(w => w.DoneExerciseItems)
                    .ThenInclude(e => e.ExerciseItem)
                .Where(w => w.UserId.Equals(_userId));
            foreach (var doneWorkout in doneWorkouts)
            {
                if (doneWorkout.State == StoreItemState.CreatedClientside)
                {
                    var response = await nS.PostDoneWorkout(_userId, doneWorkout);
                    if (response.IsSuccessStatusCode)
                    {
                        doneWorkout.State = StoreItemState.EqualsWithServer;
                    }
                    else
                        return new SyncMessage() { Result = SyncronizationMessages.ModifiedInstancesUploadFail };
                }
            }
            _context.DoneWorkouts.UpdateRange(doneWorkouts);

            await _context.SaveChangesAsync();
            return new SyncMessage()
            {
                IsSucessfull = true,
                Result = SyncronizationMessages.ModifiedInstancesUploadSucess
            };
        }
        private async Task<SyncMessage> DownloadUserWorkoutsAsync(NetworkService nS)
        {
            try
            {
                List<Workout> sWorkouts = await nS.GetWorkouts(_userId);
                List<Workout> cWorkouts = _context.Workouts.ToList();
                foreach (var sWorkout in sWorkouts)
                {
                    var cWorkout = cWorkouts.SingleOrDefault(w => w.ServerId.Equals(sWorkout.Id));
                    if (cWorkout == null)
                    {
                        cWorkout = new Workout
                        {
                            Difficulty = sWorkout.Difficulty,
                            State = StoreItemState.EqualsWithServer,
                            Name = sWorkout.Name,
                            UserId = _userId,
                            ServerId = sWorkout.Id,
                            ExerciseItems = new List<ExerciseItem>()
                        };
                        foreach (var item in sWorkout.ExerciseItems)
                        {
                            var newExerciseItem = new ExerciseItem
                            {
                                Exercise = _context.Exercises.SingleOrDefault(e => e.Name.Equals(item.Exercise.Name)),
                                Reps = item.Reps,
                                State = StoreItemState.EqualsWithServer,
                                ServerId = item.Id,
                                Serial = item.Serial
                            };
                            cWorkout.ExerciseItems.Add(newExerciseItem);
                        }
                        _context.Workouts.Add(cWorkout);
                    }
                }
                await _context.SaveChangesAsync();
                return new SyncMessage { Result = SyncronizationMessages.DownloadInstancesFromServerSucess, IsSucessfull = true };
            }
            catch
            {
                return new SyncMessage { Result = SyncronizationMessages.DownloadInstancesFromServerFail };
            }
        }
    }
}
