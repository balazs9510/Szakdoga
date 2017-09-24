package hu.bme.aut.workoutplaner;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TextView;
import android.widget.Toast;

import java.net.ProtocolException;

import butterknife.BindView;
import butterknife.ButterKnife;
import hu.bme.aut.workoutplaner.model.WeeklyWorkout;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;
import okhttp3.ResponseBody;

public class WeeklyWorkoutDescriptionActivity extends AppCompatActivity {
    @BindView(R.id.tv_day_one)
    TextView tv_1;
    @BindView(R.id.tv_day_two)
    TextView tv_2;
    @BindView(R.id.tv_day_three)
    TextView tv_3;
    @BindView(R.id.tv_day_four)
    TextView tv_4;
    @BindView(R.id.tv_day_five)
    TextView tv_5;
    @BindView(R.id.tv_day_six)
    TextView tv_6;
    @BindView(R.id.tv_day_seven)
    TextView tv_7;
    @BindView(R.id.tv_wworkout_name)
    TextView name;
    private WeeklyWorkout current;
    private int _id;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_weekly_workout_description);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar_weekly);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        ButterKnife.bind(this);
        _id = getIntent().getExtras().getInt("id");
        loadWeeklyWorkout();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_weekly_workout_description_menu, menu);
        return true;
    }
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_delete_weekly_workout) {
            WorkoutInteractor workoutInteractor = new WorkoutInteractor(this);
            workoutInteractor.deleteWeeklyWorkout(new WorkoutInteractor.ResponseListener<ResponseBody>() {
                @Override
                public void onResponse(ResponseBody responseBody) {
                    Toast.makeText(WeeklyWorkoutDescriptionActivity.this,"Sikeres törlés",Toast.LENGTH_SHORT).show();
                    finish();
                }

                @Override
                public void onError(Exception e) {
                    if(e instanceof ProtocolException)
                        Toast.makeText(WeeklyWorkoutDescriptionActivity.this,
                                "Ez az elem nem törölhető, mert egy másik edzés része.",Toast.LENGTH_LONG).show();
                }
            },current.getId());
        }
        return super.onOptionsItemSelected(item);
    }


    private void loadWeeklyWorkout() {
        WorkoutInteractor wi = new WorkoutInteractor(this);
        wi.getWeeklyWorkout(new WorkoutInteractor.ResponseListener<WeeklyWorkout>() {
            @Override
            public void onResponse(WeeklyWorkout weeklyWorkout) {
                current = weeklyWorkout;
                if (current.getDayOne() != null)
                    tv_1.setText(current.getDayOne().getName());
                if (current.getDayTwo() != null)
                    tv_2.setText(current.getDayTwo().getName());
                if (current.getDayThree() != null)
                    tv_3.setText(current.getDayThree().getName());
                if (current.getDayFour() != null)
                    tv_4.setText(current.getDayFour().getName());
                if (current.getDayFive() != null)
                    tv_5.setText(current.getDayFive().getName());
                if (current.getDaySix() != null)
                    tv_6.setText(current.getDaySix().getName());
                if (current.getDaySeven() != null)
                    tv_7.setText(current.getDaySeven().getName());
                name.setText(current.getName());
            }

            @Override
            public void onError(Exception e) {

            }
        }, _id);
    }

}
