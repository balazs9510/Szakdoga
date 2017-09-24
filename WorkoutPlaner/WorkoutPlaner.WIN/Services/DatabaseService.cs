using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.Services;
using WorkoutPlaner.WIN.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseService))]
namespace WorkoutPlaner.WIN.Services
{
    public class DatabaseService : IDatabaseService
    {
        public string GetDatabaseConnectionString()
        {
            return "Data Source=WorkoutPlaner.db";
        }
    }
}
