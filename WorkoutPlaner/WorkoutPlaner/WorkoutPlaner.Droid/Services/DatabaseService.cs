using System.IO;
using WorkoutPlaner.Droid.Services;
using WorkoutPlaner.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseService))]
namespace WorkoutPlaner.Droid.Services
{
    public class DatabaseService : IDatabaseService
    {
        public string GetDatabaseConnectionString()
        {
            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fileName = "WorkoutPlaner.db";
            var dbFullPath = Path.Combine(dbFolder, fileName);
            return $"Filename={dbFullPath}";
        }
    }
}