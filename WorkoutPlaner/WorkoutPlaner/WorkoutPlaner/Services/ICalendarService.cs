using System;

namespace WorkoutPlaner.Services
{
    public interface ICalendarService
    {
        void SaveWorkoutAsync(string workoutName, DateTime timeOfWorkout);
    }
}
