using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.BLL.Model;
using WorkoutPlaner.DAL.Model;
using Xamarin.Forms;

namespace WorkoutPlaner.Services
{
    public class NetworkService
    {
        private HttpClient client;
        public string AuthCookie { get; set; }
        private readonly Uri serverUrl;
        public NetworkService()
        {
            if (Device.RuntimePlatform.Equals(Device.Android))
                //serverUrl = new Uri("http://127.0.0.1:65175/");
                serverUrl = new Uri("http://10.0.2.2:65175/");
            else
                serverUrl = new Uri("http://127.0.0.1:65175/");
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000,
                Timeout = TimeSpan.FromSeconds(1000)
            };
            AuthCookie = string.Empty;
        }
        #region GET
        private async Task<T> GetAsync<T>(Uri uri)
        {
            string json = "";
            foreach (var item in App.AuthCookies)
            {
                client.DefaultRequestHeaders.Add("Cookie", item);
            }
            
            var response = await client.GetAsync(uri);
            json = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        public async Task<ApplicationUser> GetUser(string id)
        {
            return await GetAsync<ApplicationUser>(new Uri(serverUrl, $"api/User/{id}"));
        }
        public async Task<List<Workout>> GetWorkouts(string userId)
        {
            return await GetAsync<List<Workout>>(new Uri(serverUrl, $"User/GetWorkouts/{userId}"));
        }
        #endregion
        #region POST
        /// <summary>Base POST method </summary>
        /// <typeparam name="T">type of data</typeparam>
        /// <param name="url">Url of request</param>
        /// <param name="data">The data to post</param>
        /// <returns>The message of the server</returns>
        public async Task<HttpResponseMessage> PostAsync<T>(Uri url, T data)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                var httpcontent = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                foreach (var item in App.AuthCookies)
                {
                    client.DefaultRequestHeaders.Add("Cookie", item);
                }
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync(url, httpcontent);
                Debug.WriteLine(httpcontent.ToString());

                return response;
            }
        }
        public async Task<HttpResponseMessage> PostAsync(Uri url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(""));
                return response;
            }
        }
        public async Task<HttpResponseMessage> PostLogin(LoginViewModel lvm)
        {
            var response = await PostAsync<LoginViewModel>(new Uri(serverUrl, "/Account/Login"), lvm);
            return response;
        }
        public async Task<HttpResponseMessage> PostRegister(RegisterViewModel lvm)
        {
            var response = await PostAsync(new Uri(serverUrl, "/Account/Register"), lvm);
            return response;
        }
        public async Task<HttpResponseMessage> PostWorkout(string userId, Workout w)
        {
            var response = await PostAsync(new Uri(serverUrl, $"/api/Workouts/{userId}"), w);
            return response;
        }
        public async Task<HttpResponseMessage> PostDoneWorkout(string userId, DoneWorkout w)
        {
            var response = await PostAsync(new Uri(serverUrl, $"/api/DoneWorkouts/{userId}"), w);
            return response;
        }
        public async Task<HttpResponseMessage> PostLogout()
        {
            var response = await PostAsync(new Uri(serverUrl, $"/Account/Logout"));
            return response;
        }
        #endregion
        #region PUT
        private async Task<HttpResponseMessage> PutAsync<T>(Uri uri, string id, T data)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                foreach (var item in App.AuthCookies)
                {
                    client.DefaultRequestHeaders.Add("Cookie", item);
                }
                var httpcontent = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PutAsync(uri + $"/{id}", httpcontent);
                return response;
            }
        }
        public async Task<HttpResponseMessage> PutWorkout(string userId, Workout w)
        {
            var response = await PutAsync(new Uri(serverUrl, $"api/Workouts"), userId, w);
            return response;
        }
        #endregion
        #region DELETE
        public async Task<HttpResponseMessage> DeleteAsync(Uri uri, string id)
        {
            using (var client = new HttpClient())
            {
                foreach (var item in App.AuthCookies)
                {
                    client.DefaultRequestHeaders.Add("Cookie", item);
                }
                var response = await client.DeleteAsync(uri + $"/{id}");
                return response;
            }
        }
        public async Task<HttpResponseMessage> DeleteWorkout(string workoutId)
        {
            var response = await DeleteAsync(new Uri(serverUrl, "/api/Workouts"), workoutId);
            return response;
        }
        #endregion
        public async Task<T> GetObjectFromResponseAsync<T>(HttpResponseMessage response)
        {
            string json = string.Empty;
            json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
