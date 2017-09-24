package hu.bme.aut.workoutplaner;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.net.ConnectException;
import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import hu.bme.aut.workoutplaner.adapter.ExerciseAdapter;
import hu.bme.aut.workoutplaner.adapter.ExerciseItemAdapter;
import hu.bme.aut.workoutplaner.fragment.AddExerciseItemDialogFragment;
import hu.bme.aut.workoutplaner.model.BaseWorkout;
import hu.bme.aut.workoutplaner.model.DailyWorkout;
import hu.bme.aut.workoutplaner.model.Exercise;
import hu.bme.aut.workoutplaner.model.ExerciseItem;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;
import okhttp3.ResponseBody;

public class AddDailyWorkoutActivity extends AppCompatActivity implements AddExerciseItemDialogFragment.AddedExerciseItemInterface {
    @BindView(R.id.et_add_dworkout_name)
    EditText et_name;
    @BindView(R.id.et_search_exercise)
    EditText et_search;
    @BindView(R.id.rv_list_exercises)
    RecyclerView rv_exercise;
    @BindView(R.id.rv_added_exercises)
    RecyclerView rv_add_exercises;
    @BindView(R.id.btn_save_dworkout)
    Button btn_save;
    @BindView(R.id.btn_cancel_dworkout)
    Button btn_cancel;
    ExerciseAdapter adapter;
    ExerciseItemAdapter ei_adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_daily_workout);
        ButterKnife.bind(this);
        LinearLayoutManager lm = new LinearLayoutManager(this);
        rv_exercise.setLayoutManager(lm);
        adapter = new ExerciseAdapter(this, getSupportFragmentManager(), new ArrayList<Exercise>(), true);
        rv_exercise.setAdapter(adapter);
        ei_adapter = new ExerciseItemAdapter(this,getSupportFragmentManager(),true);
        LinearLayoutManager lm2 = new LinearLayoutManager(this);
        rv_add_exercises.setLayoutManager(lm2);
        rv_add_exercises.setAdapter(ei_adapter);
        btn_cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
        btn_save.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (modelStateIsValid()) {
                    DailyWorkout saveInstance = new DailyWorkout(0,
                            et_name.getText().toString(), BaseWorkout.Type.Daily,ei_adapter.getExerciseItems());
                    WorkoutInteractor workoutInteractor = new WorkoutInteractor(getBaseContext());
                    workoutInteractor.postDailyWorkout(saveInstance, new WorkoutInteractor.ResponseListener<ResponseBody>() {
                        @Override
                        public void onResponse(ResponseBody responseBody) {
                            Toast.makeText(AddDailyWorkoutActivity.this, "Sikeres feltöltés!", Toast.LENGTH_SHORT).show();
                        }

                        @Override
                        public void onError(Exception e) {
                            if(e instanceof ConnectException){
                                Toast.makeText(getBaseContext(),"Nem elérhető a hálózat",Toast.LENGTH_SHORT).show();
                            }
                            e.printStackTrace();
                        }
                    });
                    finish();
                } else
                    Toast.makeText(getBaseContext(), getString(R.string.error_msg_1), Toast.LENGTH_LONG).show();
            }
        });
        loadExercises();
        
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

    }
    @OnClick(R.id.btn_search)
    public void searchExercises(){
        adapter.search(et_search.getText().toString());
        rv_exercise.setAdapter(adapter);
    }
    private boolean modelStateIsValid() {
        return !et_name.getText().toString().trim().isEmpty() &&
                (et_name.getText().toString().length() > 3) &&
                ei_adapter.getItemCount() > 0;
    }

    private void loadExercises() {
        WorkoutInteractor wi = new WorkoutInteractor(this);
        wi.getExercises(new WorkoutInteractor.ResponseListener<List<Exercise>>() {
            @Override
            public void onResponse(List<Exercise> exercises) {
                adapter.setExercises(exercises);
                rv_exercise.setAdapter(adapter);
            }

            @Override
            public void onError(Exception e) {
                if (e instanceof ConnectException) {
                    Toast.makeText(getBaseContext(), "Nem elérhető a hálózat", Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    @Override
        public void AddedExerciseItem(ExerciseItem item,boolean edited,int pos) {
            if(pos!=-1)
                ei_adapter.removeExerciseItem(pos);
            ei_adapter.addExecriseItem(item);
            rv_add_exercises.setAdapter(ei_adapter);

        }
}
