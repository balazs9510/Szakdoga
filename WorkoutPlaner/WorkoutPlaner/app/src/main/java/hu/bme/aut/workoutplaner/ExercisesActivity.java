package hu.bme.aut.workoutplaner;

import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Toast;

import java.net.ConnectException;
import java.util.List;

import hu.bme.aut.workoutplaner.adapter.ExerciseAdapter;
import hu.bme.aut.workoutplaner.fragment.AddExerciseDialogFragment;
import hu.bme.aut.workoutplaner.model.Exercise;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;

public class ExercisesActivity extends AppCompatActivity implements AddExerciseDialogFragment.ExerciseAddedInterface {
    private ExerciseAdapter adapter;
    RecyclerView exercisesRV;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_exercises);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar_exercise);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);        LinearLayoutManager mLayoutManager = new LinearLayoutManager(this);
        exercisesRV = (RecyclerView) findViewById(R.id.rv_exercises);
        exercisesRV.setLayoutManager(mLayoutManager);
        loadExercises();
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.activity_exercise_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.menu_new_exercise) {
            DialogFragment fragment = new AddExerciseDialogFragment(this);
            fragment.show(getSupportFragmentManager(),"dialog");
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    private void loadExercises(){
        WorkoutInteractor wi = new WorkoutInteractor(this);
        wi.getExercises(new WorkoutInteractor.ResponseListener<List<Exercise>>() {
            @Override
            public void onResponse(List<Exercise> exercises) {
                adapter = new ExerciseAdapter(getApplicationContext(), getSupportFragmentManager(), exercises,false);
                exercisesRV.setAdapter(adapter);
            }
            @Override
            public void onError(Exception e) {
                if(e instanceof ConnectException){
                    Toast.makeText(getBaseContext(),"Nem elérhető a hálózat",Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    @Override
    public void onExerciseAdded(Exercise e) {
        adapter.exerciseAdd(e);
        exercisesRV.setAdapter(adapter);
    }
}
