package hu.bme.aut.workoutplaner.fragment;


import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatDialogFragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.List;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.adapter.WorkoutRecyclerViewAdapter;
import hu.bme.aut.workoutplaner.model.WeeklyWorkout;

/**
 * A simple {@link Fragment} subclass.
 */
public class SelectWorkoutDialogFragment extends AppCompatDialogFragment {

    private WorkoutRecyclerViewAdapter adapter;
    private Context context;
    private RecyclerView rv;


    public SelectWorkoutDialogFragment(Context context) {
        this.context = context;
        adapter = new WorkoutRecyclerViewAdapter(context);

    }

    public WorkoutRecyclerViewAdapter getAdapter() {
        return adapter;
    }

    public void setAdapterWeeklyWorkoutsItems(List<WeeklyWorkout> wws) {
        adapter.setWeeklyWorkouts(wws);
    }

    public SelectWorkoutDialogFragment() {


    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_select_daliy_workout_dialog, container, false);
        LinearLayoutManager lm = new LinearLayoutManager(context);
        rv = (RecyclerView) v.findViewById(R.id.rv_add_daily_workout);
        rv.setLayoutManager(lm);
        rv.setAdapter(adapter);
        return v;
    }

}
