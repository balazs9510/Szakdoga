using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WorkoutPlaner.Server.DAL.Models;
using WorkoutPlaner.Server.Data;

namespace WorkoutPlaner.Server.DAL.Seed
{
    public static class SeedDatabase
    {
        public static void Seed(this ApplicationDbContext context)
        {
            var random = new Random();
            if (!context.MuscleGroups.Any())
            {
                var ms = LoadMuscleGroups();
                foreach (var muscleGroup in ms)
                {
                    context.MuscleGroups.Add(muscleGroup);
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                var exercises = LoadExercises();
                foreach (var exercise in exercises)
                {
                    exercise.MuscleGroup = context.MuscleGroups.SingleOrDefault(m => m.Name.Equals(exercise.MuscleGroup.Name));
                    exercise.Difficulty = random.Next() % 5 + 1;
                    context.Exercises.Add(exercise);
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        private static List<MuscleGroup> LoadMuscleGroups()
        {
               var assembly = typeof(SeedDatabase).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WorkoutPlaner.Server.DAL.Seed.MuscleGroups.json");
            using (StreamReader r = new StreamReader(stream, Encoding.UTF8, true))
            {
                string json = r.ReadToEnd();
                List<MuscleGroup> items = JsonConvert.DeserializeObject<List<MuscleGroup>>(json);
                items.Sort((a, b) => a.Name.CompareTo(b.Name));
                return items;
            }
        }
        private static List<Exercise> LoadExercises()
        {
            var assembly = typeof(SeedDatabase).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WorkoutPlaner.Server.DAL.Seed.Exercises.json");
            using (StreamReader r = new StreamReader(stream, Encoding.UTF8))
            {
                string json = r.ReadToEnd();
                List<Exercise> items = JsonConvert.DeserializeObject<List<Exercise>>(json);
                items.Sort((a, b) => a.Name.CompareTo(b.Name));
                return items;
            }
        }
    }
}
