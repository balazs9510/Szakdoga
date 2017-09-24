package hu.bme.aut.workoutplaner.fragment;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import java.net.ConnectException;
import java.util.List;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.adapter.WorkoutRecyclerViewAdapter;
import hu.bme.aut.workoutplaner.model.DailyWorkout;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public class DailyWorkoutFragment extends Fragment {
    private RecyclerView rv;
    private WorkoutRecyclerViewAdapter adapter;
    private Context context;

    public DailyWorkoutFragment() {
        this.context = getContext();
        adapter=new  WorkoutRecyclerViewAdapter(context);

    }

    public DailyWorkoutFragment(Context context) {
        this.context = context;
        adapter=new  WorkoutRecyclerViewAdapter(context);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_workout,container,false);
        rv = (RecyclerView) v.findViewById(R.id.rv_workout);
        LinearLayoutManager llm = new LinearLayoutManager(context);
        rv.setLayoutManager(llm);
        rv.setAdapter(adapter);
        loadWorkouts();
        return v;
    }

    private void loadWorkouts() {
        WorkoutInteractor wi = new WorkoutInteractor(getContext());

        wi.getDailyWorkouts(new WorkoutInteractor.ResponseListener<List<DailyWorkout>>() {
            @Override
            public void onResponse(List<DailyWorkout> weeklyWorkouts) {
                adapter.setDailyWorkouts(weeklyWorkouts);
                rv.setAdapter(adapter);
            }

            @Override
            public void onError(Exception e) {
                if(e instanceof ConnectException){
                    Toast.makeText(getContext(),"Nem elérhető a hálózat",Toast.LENGTH_LONG).show();
                }
            }
        });

    }
}
