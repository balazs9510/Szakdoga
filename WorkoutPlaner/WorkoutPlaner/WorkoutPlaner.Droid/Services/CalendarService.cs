using Android.Content;
using Android.Provider;
using Java.Util;
using System;
using WorkoutPlaner.Droid.Services;
using WorkoutPlaner.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalendarService))]
namespace WorkoutPlaner.Droid.Services
{
    public class CalendarService : ICalendarService
    {
        public void SaveWorkoutAsync(string workoutName, DateTime timeOfWorkout)
        {
            Intent intent = new Intent(Intent.ActionInsert);
            intent.SetData(CalendarContract.Events.ContentUri);
            intent.PutExtra(CalendarContract.ExtraEventBeginTime,
                GetDateTimeMS(timeOfWorkout.Year, timeOfWorkout.Month, timeOfWorkout.Day, timeOfWorkout.Hour, timeOfWorkout.Minute));
            intent.PutExtra(CalendarContract.ExtraEventBeginTime,
                GetDateTimeMS(timeOfWorkout.Year, timeOfWorkout.Month, timeOfWorkout.Day, timeOfWorkout.Hour + 2, timeOfWorkout.Minute));
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Title,
                workoutName);
            try
            {
                Forms.Context.StartActivity(intent);
                return;
            }catch(Exception e)
            {
                return;
            }
        }
        long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {

            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Calendar.DayOfMonth, 15);
            c.Set(Calendar.HourOfDay, hr);
            c.Set(Calendar.Minute, min);
            c.Set(Calendar.Month, Calendar.December);
            c.Set(Calendar.Year, 2011);

            return c.TimeInMillis;
        }
    }
}