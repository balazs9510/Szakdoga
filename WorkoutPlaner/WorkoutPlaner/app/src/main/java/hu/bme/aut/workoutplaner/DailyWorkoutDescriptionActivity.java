package hu.bme.aut.workoutplaner;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TextView;
import android.widget.Toast;

import java.net.ConnectException;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import hu.bme.aut.workoutplaner.adapter.ExerciseItemAdapter;
import hu.bme.aut.workoutplaner.fragment.CalendarDateTimeDialogFragment;
import hu.bme.aut.workoutplaner.model.DailyWorkout;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;
import okhttp3.ResponseBody;

public class DailyWorkoutDescriptionActivity extends AppCompatActivity {
    @BindView(R.id.tv_dworkout_name_description)
    TextView tv_name;
    @BindView(R.id.rv_dworkout_exercise_items)
    RecyclerView rv_items;

    private DailyWorkout current;
    int dworkout_id;
    ExerciseItemAdapter adapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Bundle callerData = getIntent().getExtras();
        dworkout_id = callerData.getInt("id");
        setContentView(R.layout.activity_daily_workout_descripiton);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar_daily);
        setSupportActionBar(toolbar);
        ButterKnife.bind(this);

        adapter = new ExerciseItemAdapter(this, getSupportFragmentManager(),false);
        LinearLayoutManager lm = new LinearLayoutManager(this);
        rv_items.setLayoutManager(lm);
        loadDailyWorkout();
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);    }
    @OnClick(R.id.btn_save_to_calendar)
    public void importToCalendar(){
        CalendarDateTimeDialogFragment df = new CalendarDateTimeDialogFragment(this,current.getName());
        df.show(getSupportFragmentManager(),"Calendar");
    }
    private void loadDailyWorkout(){
        WorkoutInteractor wi = new WorkoutInteractor(this);
        wi.getDailyWorkout(new WorkoutInteractor.ResponseListener<DailyWorkout>() {
            @Override
            public void onResponse(DailyWorkout dailyWorkout) {
                current = dailyWorkout;
                tv_name.setText(current.getName());
                adapter.setExerciseItems(current.getExercises());
                rv_items.setAdapter(adapter);
            }

            @Override
            public void onError(Exception e) {
                if(e instanceof ConnectException){
                    Toast.makeText(DailyWorkoutDescriptionActivity.this,"Nem elérhető a hálózat",Toast.LENGTH_SHORT).show();
                }
                e.printStackTrace();
            }
        },String.valueOf(dworkout_id));
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_daily_workout_description_menu, menu);

        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_delete_daily_workout) {
            WorkoutInteractor workoutInteractor = new WorkoutInteractor(this);
            workoutInteractor.deleteDailyWorkout(new WorkoutInteractor.ResponseListener<ResponseBody>() {
                @Override
                public void onResponse(ResponseBody responseBody) {
                    Toast.makeText(DailyWorkoutDescriptionActivity.this,"Sikeres törlés",Toast.LENGTH_SHORT);
                    finish();
                }

                @Override
                public void onError(Exception e) {

                }
            },current.getId());
        }


        return super.onOptionsItemSelected(item);
    }

}
