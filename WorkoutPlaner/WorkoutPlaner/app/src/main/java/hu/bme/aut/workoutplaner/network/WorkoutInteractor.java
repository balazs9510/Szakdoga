package hu.bme.aut.workoutplaner.network;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Handler;
import android.widget.Toast;

import com.google.gson.Gson;

import java.net.ConnectException;
import java.util.List;

import hu.bme.aut.workoutplaner.model.DailyWorkout;
import hu.bme.aut.workoutplaner.model.Exercise;
import hu.bme.aut.workoutplaner.model.MonthlyWorkout;
import hu.bme.aut.workoutplaner.model.WeeklyWorkout;
import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by Balazs on 2017. 04. 21..
 */

public class WorkoutInteractor {
    private final WorkoutAPI workoutApi;
    private final Context context;
    boolean networkAvailable;

    public WorkoutInteractor(Context context) {

        this.context = context;
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(WorkoutAPI.ENDPOINT_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        networkAvailable = activeNetworkInfo != null && activeNetworkInfo.isConnected();
        this.workoutApi = retrofit.create(WorkoutAPI.class);
    }

    public interface ResponseListener<T> {
        void onResponse(T t);

        void onError(Exception e);
    }

    private void showMessage() {
        Toast.makeText(context, "Nincs internet az eszközön.", Toast.LENGTH_SHORT);

    }

    private static <T> void runCallOnBackgroundThread(final Call<T> call, final ResponseListener<T> listener) {
        final Handler handler = new Handler();
        new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    final T response = call.execute().body();
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            listener.onResponse(response);
                        }
                    });

                } catch (final ConnectException ce) {
                    ce.printStackTrace();
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            listener.onError(ce);
                        }
                    });
                } catch (final Exception e) {
                    e.printStackTrace();
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            listener.onError(e);
                        }
                    });
                }
            }
        }).start();
    }

    public void getExercises(ResponseListener<List<Exercise>> responseListener) {
        if (networkAvailable) {
            Call<List<Exercise>> getExercisesRequest = workoutApi.getExercises();
            runCallOnBackgroundThread(getExercisesRequest, responseListener);
        } else
            showMessage();

    }

    public void getDailyWorkouts(ResponseListener<List<DailyWorkout>> responseListener) {
        if (networkAvailable) {
            Call<List<DailyWorkout>> getExercisesRequest = workoutApi.getDailyWorkouts();
            runCallOnBackgroundThread(getExercisesRequest, responseListener);
        } else
            showMessage();
    }

    public void getWeeklyWorkouts(ResponseListener<List<WeeklyWorkout>> responseListener) {
        if (networkAvailable) {

            Call<List<WeeklyWorkout>> getExercisesRequest = workoutApi.getWeeklyWorkouts();
            runCallOnBackgroundThread(getExercisesRequest, responseListener);
        } else
            showMessage();
    }

    public void getMonthlyWorkouts(ResponseListener<List<MonthlyWorkout>> responseListener) {
        if (networkAvailable) {

            Call<List<MonthlyWorkout>> getExercisesRequest = workoutApi.getMonthlyWorkouts();
            runCallOnBackgroundThread(getExercisesRequest, responseListener);
        } else
            showMessage();
    }

    public void getDailyWorkout(ResponseListener<DailyWorkout> responseListener, String id) {
        if (networkAvailable) {

            Call<DailyWorkout> getDailyWorkoutRequest = workoutApi.getDailyWorkout(id);
            runCallOnBackgroundThread(getDailyWorkoutRequest, responseListener);
        } else
            showMessage();
    }

    public void getWeeklyWorkout(ResponseListener<WeeklyWorkout> responseListener, int id) {
        if (networkAvailable) {
            Call<WeeklyWorkout> getDailyWorkoutRequest = workoutApi.getWeeklyWorkout(id);
            runCallOnBackgroundThread(getDailyWorkoutRequest, responseListener);
        } else
            showMessage();
    }

    public void getMonthlyWorkout(ResponseListener<MonthlyWorkout> responseListener, int id) {
        if (networkAvailable) {
            Call<MonthlyWorkout> getDailyWorkoutRequest = workoutApi.getMonthlyWorkout(id);
            runCallOnBackgroundThread(getDailyWorkoutRequest, responseListener);
        } else
            showMessage();
    }

    public void postDailyWorkout(DailyWorkout dw, ResponseListener<ResponseBody> responseListener) {
        if (networkAvailable) {
            Gson gson = new Gson();
            String json = gson.toJson(dw);
            RequestBody body = RequestBody.create(okhttp3.MediaType.parse("application/json; charset=utf-8"), (json));
            Call<ResponseBody> postDw = workoutApi.postDailyWorkout(body);
            runCallOnBackgroundThread(postDw, responseListener);
        } else
            showMessage();
    }

    public void postWeeklyWorkout(WeeklyWorkout ww, ResponseListener<ResponseBody> responseListener) {
        if (networkAvailable) {
            Gson gson = new Gson();
            String json = gson.toJson(ww);
            RequestBody body = RequestBody.create(okhttp3.MediaType.parse("application/json; charset=utf-8"), (json));
            Call<ResponseBody> postDw = workoutApi.postWeeklyWorkout(body);
            runCallOnBackgroundThread(postDw, responseListener);
        } else
            showMessage();
    }

    public void postMonthlyWorkout(MonthlyWorkout mw, ResponseListener<ResponseBody> responseListener) {
        if (networkAvailable) {
            Gson gson = new Gson();
            String json = gson.toJson(mw);
            RequestBody body = RequestBody.create(okhttp3.MediaType.parse("application/json; charset=utf-8"), (json));
            Call<ResponseBody> postDw = workoutApi.postMonthlyWorkout(body);
            runCallOnBackgroundThread(postDw, responseListener);
        } else
            showMessage();
    }

    public void deleteDailyWorkout(ResponseListener<ResponseBody> listener, int id) {
        if (networkAvailable) {
            Call<ResponseBody> deleteDw = workoutApi.deleteDailyWorkout(id);
            runCallOnBackgroundThread(deleteDw, listener);
        } else
            showMessage();
    }

    public void deleteWeeklyWorkout(ResponseListener<ResponseBody> listener, int id) {
        if (networkAvailable) {
            Call<ResponseBody> deleteDw = workoutApi.deleteWeeklyWorkout(id);
            runCallOnBackgroundThread(deleteDw, listener);
        } else
            showMessage();
    }

    public void deleteMonthlyWorkout(ResponseListener<ResponseBody> listener, int id) {
        if (networkAvailable) {
            Call<ResponseBody> deleteDw = workoutApi.deleteMonthlyWorkout(id);
            runCallOnBackgroundThread(deleteDw, listener);
        } else
            showMessage();
    }


}
