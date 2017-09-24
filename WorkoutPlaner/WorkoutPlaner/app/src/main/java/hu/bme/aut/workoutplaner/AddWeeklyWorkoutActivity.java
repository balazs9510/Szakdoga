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
import hu.bme.aut.workoutplaner.model.DailyWorkout;
import hu.bme.aut.workoutplaner.model.WeeklyWorkout;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;
import okhttp3.ResponseBody;

public class AddWeeklyWorkoutActivity extends AppCompatActivity implements WorkoutRecyclerViewAdapter.AddDailyWorkoutInterface {
    @BindView(R.id.tv_add_day_one)
    TextView tv_day1;
    @BindView(R.id.tv_add_day_two)
    TextView tv_day2;
    @BindView(R.id.tv_add_day_three)
    TextView tv_day3;
    @BindView(R.id.tv_add_day_four)
    TextView tv_day4;
    @BindView(R.id.tv_add_day_five)
    TextView tv_day5;
    @BindView(R.id.tv_add_day_six)
    TextView tv_day6;
    @BindView(R.id.tv_add_day_seven)
    TextView tv_day7;
    @BindView(R.id.et_add_wworkout_name)
    EditText et_name;
    int btn_pressed_id;
    private List<DailyWorkout> dailyWorkoutss;
    private WeeklyWorkout saveItem = new WeeklyWorkout(0, "", BaseWorkout.Type.Weekly);
    private SelectWorkoutDialogFragment df;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_weekly_workout);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        ButterKnife.bind(this);
        dailyWorkoutss = new ArrayList<>();
        loadDailyWorkouts();
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
    }

    @OnClick({R.id.btn_add_day1
            , R.id.btn_add_day2,
            R.id.btn_add_day3,
            R.id.btn_add_day4,
            R.id.btn_add_day5,
            R.id.btn_add_day6,
            R.id.btn_add_day7})
    public void addDailyWorkout(View view) {
        int id = view.getId();
        switch (id) {
            case R.id.btn_add_day1:
                btn_pressed_id = 1;
                showDialog();
                break;
            case R.id.btn_add_day2:
                btn_pressed_id = 2;
                showDialog();
                break;
            case R.id.btn_add_day3:
                btn_pressed_id = 3;
                showDialog();
                break;
            case R.id.btn_add_day4:
                btn_pressed_id = 4;
                showDialog();
                break;

            case R.id.btn_add_day5:
                btn_pressed_id = 5;
                showDialog();
                break;

            case R.id.btn_add_day6:
                btn_pressed_id = 6;
                showDialog();
                break;

            case R.id.btn_add_day7:
                btn_pressed_id = 7;
                showDialog();
                break;

        }
    }

    private void showDialog() {
        df = new SelectWorkoutDialogFragment(this);
        df.getAdapter().setListener(this);
        df.getAdapter().setDailyWorkouts(dailyWorkoutss);
        df.getAdapter().setSelectable(false);
        df.show(getSupportFragmentManager(), "tag");
    }

    @Override
    public void dailyWorkoutAdded(DailyWorkout dw) {
        switch (btn_pressed_id) {
            case 1:
                saveItem.setDayOne(dw);
                tv_day1.setText(dw.getName());
                df.dismiss();
                break;
            case 2:
                saveItem.setDayTwo(dw);
                tv_day2.setText(dw.getName());
                df.dismiss();
                break;
            case 3:
                saveItem.setDayThree(dw);
                tv_day3.setText(dw.getName());
                df.dismiss();
                break;
            case 4:
                saveItem.setDayFour(dw);
                tv_day4.setText(dw.getName());
                df.dismiss();
                break;
            case 5:
                saveItem.setDayFive(dw);
                tv_day5.setText(dw.getName());
                df.dismiss();
                break;
            case 6:
                saveItem.setDaySix(dw);
                tv_day6.setText(dw.getName());
                df.dismiss();
                break;
            case 7:
                saveItem.setDaySeven(dw);
                tv_day7.setText(dw.getName());
                df.dismiss();
                break;
        }

    }

    private void loadDailyWorkouts() {
        WorkoutInteractor workoutInteractor = new WorkoutInteractor(this);
        workoutInteractor.getDailyWorkouts(new WorkoutInteractor.ResponseListener<List<DailyWorkout>>() {
            @Override
            public void onResponse(List<DailyWorkout> dailyWorkouts) {
                dailyWorkoutss = dailyWorkouts;
            }

            @Override
            public void onError(Exception e) {

            }
        });
    }

    @OnClick(R.id.btn_cancel_wworkout)
    public void cancelPressed() {
        finish();
    }

    private boolean isValid() {
        return (et_name.getText().length() > 3
                && saveItem.getDayOne() != null
                && saveItem.getDayTwo() != null
                && saveItem.getDayThree() != null
                && saveItem.getDayFour() != null
                && saveItem.getDayFive() != null
                && saveItem.getDaySix() != null
                && saveItem.getDaySeven() != null
        );
    }

    @OnClick(R.id.btn_save_wworkout)
    public void savePressed() {
        if (isValid()) {
            saveItem.setName(et_name.getText().toString());
            WorkoutInteractor wi = new WorkoutInteractor(this);
            wi.postWeeklyWorkout(saveItem, new WorkoutInteractor.ResponseListener<ResponseBody>() {
                @Override
                public void onResponse(ResponseBody responseBody) {
                    Toast.makeText(AddWeeklyWorkoutActivity.this, "Sikeres feltöltés", Toast.LENGTH_SHORT).show();
                }

                @Override
                public void onError(Exception e) {
                    e.printStackTrace();
                    Toast.makeText(AddWeeklyWorkoutActivity.this, "Valami baj történt", Toast.LENGTH_SHORT).show();
                }
            });
            finish();
        } else
            Toast.makeText(AddWeeklyWorkoutActivity.this, "Legalább 4 hosszú legyen a neve és add meg az összes napot.", Toast.LENGTH_SHORT).show();
    }
}
