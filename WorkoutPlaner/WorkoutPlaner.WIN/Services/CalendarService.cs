using System;
using Windows.ApplicationModel.Appointments;
using WorkoutPlaner.Services;
using WorkoutPlaner.WIN.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalendarService))]
namespace WorkoutPlaner.WIN.Services
{
    public class CalendarService : ICalendarService
    {
        public async void SaveWorkoutAsync(string workoutName, DateTime timeOfWorkout)
        {
            var appointment = new Windows.ApplicationModel.Appointments.Appointment()
            {
                StartTime = timeOfWorkout,
                Subject = workoutName
            };
            await AppointmentManager.ShowEditNewAppointmentAsync(appointment);
        }
    }
}
