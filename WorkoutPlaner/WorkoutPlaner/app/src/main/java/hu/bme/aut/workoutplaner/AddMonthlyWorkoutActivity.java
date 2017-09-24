package hu.bme.aut.workoutplaner;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import hu.bme.aut.workoutplaner.adapter.WorkoutRecyclerViewAdapter;
import hu.bme.aut.workoutplaner.fragment.SelectWorkoutDialogFragment;
import hu.bme.aut.workoutplaner.model.BaseWorkout;
import hu.bme.aut.workoutplaner.model.MonthlyWorkout;
import hu.bme.aut.workoutplaner.model.WeeklyWorkout;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;
import okhttp3.ResponseBody;

public class AddMonthlyWorkoutActivity extends AppCompatActivity implements WorkoutRecyclerViewAdapter.AddWeeklyWorkoutInterface {
    @BindView(R.id.tv_week_one)
    TextView tv_week1;
    @BindView(R.id.tv_week_two)
    TextView tv_week2;
    @BindView(R.id.tv_week_three)
    TextView tv_week3;
    @BindView(R.id.tv_week_four)
    TextView tv_week4;
    @BindView(R.id.et_add_mworkout_name)
    EditText et_name;
    private MonthlyWorkout saveitem;
    private int btn_pressed_id;
    private SelectWorkoutDialogFragment df;
    private List<WeeklyWorkout> weeklyWorkoutss;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_monthly_workout);
        ButterKnife.bind(this);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        saveitem = new MonthlyWorkout(0, "", BaseWorkout.Type.Monthly);
        df = new SelectWorkoutDialogFragment(this);
        weeklyWorkoutss = new ArrayList<>();
        loadWeeklyWorkouts();
    }

    @OnClick({R.id.btn_monthly_workout_week_one,
            R.id.btn_monthly_workout_week_two,
            R.id.btn_monthly_workout_week_three,
            R.id.btn_monthly_workout_week_four})
    public void addDailyWorkout(View view) {
        int id = view.getId();
        switch (id) {
            case R.id.btn_monthly_workout_week_one:
                btn_pressed_id = 1;
                showDialog();
                break;
            case R.id.btn_monthly_workout_week_two:
                btn_pressed_id = 2;
                showDialog();
                break;
            case R.id.btn_monthly_workout_week_three:
                btn_pressed_id = 3;
                showDialog();
                break;

            case R.id.btn_monthly_workout_week_four:
                btn_pressed_id = 4;
                showDialog();
                break;
        }
    }

    private void showDialog() {
        df = new SelectWorkoutDialogFragment(this);
        df.getAdapter().setListener2(this);
        df.getAdapter().setWeeklyWorkouts(weeklyWorkoutss);
        df.getAdapter().setSelectable(false);
        df.show(getSupportFragmentManager(), "tag");
    }

    @Override
    public void weeklyWorkoutAdded(WeeklyWorkout ww) {
        switch (btn_pressed_id) {
            case 1:
                tv_week1.setText(ww.getName());
                saveitem.setWeekOne(ww);
                break;
            case 2:
                tv_week2.setText(ww.getName());
                saveitem.setWeekTwo(ww);
                break;
            case 3:
                tv_week3.setText(ww.getName());
                saveitem.setWeekThree(ww);
                break;
            case 4:
                tv_week4.setText(ww.getName());
                saveitem.setWeekFour(ww);
                break;
        }
        df.dismiss();
    }

    private void loadWeeklyWorkouts() {
        WorkoutInteractor workoutInteractor = new WorkoutInteractor(this);
        workoutInteractor.getWeeklyWorkouts(new WorkoutInteractor.ResponseListener<List<WeeklyWorkout>>() {
            @Override
            public void onResponse(List<WeeklyWorkout> weeklyWorkouts) {
                weeklyWorkoutss = weeklyWorkouts;
            }

            @Override
            public void onError(Exception e) {

            }
        });
    }

    @OnClick(R.id.btn_monthly_workout_cancel)
    public void cancelPresserd() {
        finish();
    }

    private boolean isValid() {
        return (et_name.getText().toString().length() > 3 &&
                saveitem.getWeekOne() != null &&
                saveitem.getWeekTwo() != null &&
                saveitem.getWeekThree() != null &&
                saveitem.getWeekFour() != null);
    }

    @OnClick(R.id.btn_monthly_workout_save)
    public void savePressed() {
        if (isValid()) {
            saveitem.setName(et_name.getText().toString());
            WorkoutInteractor wi = new WorkoutInteractor(this);
            wi.postMonthlyWorkout(saveitem, new WorkoutInteractor.ResponseListener<ResponseBody>() {
                @Override
                public void onResponse(ResponseBody responseBody) {
                    Toast.makeText(AddMonthlyWorkoutActivity.this, "Sikeres feltöltés", Toast.LENGTH_SHORT).show();
                }

                @Override
                public void onError(Exception e) {
                    e.printStackTrace();
                    Toast.makeText(AddMonthlyWorkoutActivity.this, "Valami baj történt", Toast.LENGTH_SHORT).show();
                }
            });
            finish();
        } else
            Toast.makeText(AddMonthlyWorkoutActivity.this, "Legalább 4 hosszú legyen a neve és add meg az összes hetet.", Toast.LENGTH_SHORT).show();


    }
}
