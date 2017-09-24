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
import hu.bme.aut.workoutplaner.model.MonthlyWorkout;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;
import okhttp3.ResponseBody;

public class MonthlyWorkoutDescriptionActivity extends AppCompatActivity {
    
    private MonthlyWorkout current;
    private int _id;
    @BindView(R.id.tv_week_desc_one)
    TextView tv_week1;
    @BindView(R.id.tv_week_desc_two)
    TextView tv_week2;
    @BindView(R.id.tv_week_desc_three)
    TextView tv_week3;
    @BindView(R.id.tv_week_desc_four)
    TextView tv_week4;
    @BindView(R.id.tv_mworkout_name)
    TextView tv_name;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_monthly_workout_description);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        ButterKnife.bind(this);
        _id = getIntent().getExtras().getInt("id");
        loadWorkout();
    }

    private void loadWorkout() {
        WorkoutInteractor wi = new WorkoutInteractor(this);
        wi.getMonthlyWorkout(new WorkoutInteractor.ResponseListener<MonthlyWorkout>() {
            @Override
            public void onResponse(MonthlyWorkout monthlyWorkout) {
                current = monthlyWorkout;
                if(current.getWeekOne()!=null)
                    tv_week1.setText(current.getWeekOne().getName());
                if(current.getWeekTwo()!=null)
                    tv_week2.setText(current.getWeekTwo().getName());
                if(current.getWeekThree()!=null)
                    tv_week3.setText(current.getWeekThree().getName());
                if(current.getWeekFour()!=null)
                    tv_week4.setText(current.getWeekFour().getName());
                tv_name.setText(current.getName());
            }

            @Override
            public void onError(Exception e) {

            }
        }, _id);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_monthly_workout_descrition_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.action_delete_monthly_workout) {
            WorkoutInteractor wi = new WorkoutInteractor(this);
            wi.deleteMonthlyWorkout(new WorkoutInteractor.ResponseListener<ResponseBody>() {
                @Override
                public void onResponse(ResponseBody responseBody) {
                    Toast.makeText(MonthlyWorkoutDescriptionActivity.this, "Sikeres törlés", Toast.LENGTH_SHORT).show();
                    finish();
                }

                @Override
                public void onError(Exception e) {
                    if (e instanceof ProtocolException)
                        Toast.makeText(MonthlyWorkoutDescriptionActivity.this,
                                "Valami baj történt.", Toast.LENGTH_LONG).show();
                }
            }, _id);
            finish();
        }
        return super.onOptionsItemSelected(item);
    }
}
