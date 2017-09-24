package hu.bme.aut.workoutplaner.network;

import java.util.List;

import hu.bme.aut.workoutplaner.model.DailyWorkout;
import hu.bme.aut.workoutplaner.model.Exercise;
import hu.bme.aut.workoutplaner.model.MonthlyWorkout;
import hu.bme.aut.workoutplaner.model.WeeklyWorkout;
import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

/**
 * Created by Balazs on 2017. 04. 21..
 */

public interface WorkoutAPI {
     //String ENDPOINT_URL = "http://192.168.137.1:80";
    String ENDPOINT_URL = "http://10.0.2.2:65175";

    @GET("api/exercises")
    Call<List<Exercise>> getExercises();

    @GET("api/dailyworkouts")
    Call<List<DailyWorkout>> getDailyWorkouts();

    @GET("api/weeklyworkouts")
    Call<List<WeeklyWorkout>> getWeeklyWorkouts();

    @GET("api/monthlyworkouts")
    Call<List<MonthlyWorkout>> getMonthlyWorkouts();

    @GET("api/dailyworkouts/{id}")
    Call<DailyWorkout> getDailyWorkout(@Path("id") String id);

    @GET("api/weeklyworkouts/{id}")
    Call<WeeklyWorkout> getWeeklyWorkout(@Path("id") int id);

    @GET("api/monthlyworkouts/{id}")
    Call<MonthlyWorkout> getMonthlyWorkout(@Path("id") int id);

    @POST("api/dailyworkouts")
    Call<ResponseBody> postDailyWorkout(@Body RequestBody dworkout);

    @POST("api/weeklyworkouts")
    Call<ResponseBody> postWeeklyWorkout(@Body RequestBody wworkout);

    @POST("api/monthlyworkouts")
    Call<ResponseBody> postMonthlyWorkout(@Body RequestBody mworkout);

    @DELETE("api/dailyworkouts/{id}")
    Call<ResponseBody> deleteDailyWorkout(@Path("id") int id);

    @DELETE("api/weeklyworkouts/{id}")
    Call<ResponseBody> deleteWeeklyWorkout(@Path("id") int id);

    @DELETE("api/monthlyworkouts/{id}")
    Call<ResponseBody> deleteMonthlyWorkout(@Path("id") int id);

}
