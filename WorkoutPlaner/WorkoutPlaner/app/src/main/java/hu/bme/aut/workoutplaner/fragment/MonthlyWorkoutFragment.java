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
import hu.bme.aut.workoutplaner.model.MonthlyWorkout;
import hu.bme.aut.workoutplaner.network.WorkoutInteractor;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public class MonthlyWorkoutFragment extends Fragment {
    private RecyclerView rv;
    private WorkoutRecyclerViewAdapter adapter;
    private Context context;

    public MonthlyWorkoutFragment(Context context) {
        this.context = context;
        adapter = new WorkoutRecyclerViewAdapter(context);
    }


    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_workout, container, false);
        rv = (RecyclerView) v.findViewById(R.id.rv_workout);
        LinearLayoutManager llm = new LinearLayoutManager(v.getContext());
        rv.setLayoutManager(llm);
        rv.setAdapter(adapter);
        loadWorkouts();
        return v;
    }

    private void loadWorkouts() {
        WorkoutInteractor wi = new WorkoutInteractor(getContext());

        wi.getMonthlyWorkouts(new WorkoutInteractor.ResponseListener<List<MonthlyWorkout>>() {
            @Override
            public void onResponse(List<MonthlyWorkout> weeklyWorkouts) {
                adapter.setMonthlyWorkouts(weeklyWorkouts);
                rv.setAdapter(adapter);
            }

            @Override
            public void onError(Exception e) {
                if (e instanceof ConnectException) {
                    Toast.makeText(getContext(), "Nem elérhető a hálózat", Toast.LENGTH_LONG).show();
                }
            }
        });

    }
}
