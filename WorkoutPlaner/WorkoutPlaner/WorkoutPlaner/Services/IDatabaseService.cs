using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlaner.Services
{
    /// <summary>This interface helps to reach each platform database.</summary>
    public interface IDatabaseService
    {
        string GetDatabaseConnectionString();
    }
}
