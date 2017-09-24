package hu.bme.aut.workoutplaner.adapter;

import android.content.Context;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.FragmentManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.fragment.ExerciseDescriptionDialogFragment;
import hu.bme.aut.workoutplaner.fragment.AddExerciseItemDialogFragment;
import hu.bme.aut.workoutplaner.model.Exercise;

/**
 * Created by Balazs on 2017. 04. 21..
 */

public class ExerciseAdapter extends RecyclerView.Adapter<ExerciseAdapter.ExerciseViewHolder> {
    private final LayoutInflater layoutInflater;
    private final Context context;
    private final FragmentManager fragmentManager;
    private List<Exercise> exercises;
    private boolean showAddEnabled;
    private int[] serialreps;

    public ExerciseAdapter(Context context, FragmentManager fragmentManager, List<Exercise> exercises, boolean showAddEnabled) {
        this.context = context;
        this.fragmentManager = fragmentManager;
        this.exercises = exercises;
        this.layoutInflater = LayoutInflater.from(context);


        this.showAddEnabled = showAddEnabled;
        if (!showAddEnabled) {
            List<Exercise> localExercises = Exercise.listAll(Exercise.class);
            for (Exercise e : localExercises) {
                exercises.add(e);
            }
        }
        Collections.sort(exercises, new NameComparator());

    }

    public void changed() {
        notifyDataSetChanged();
    }

    @Override
    public ExerciseViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = layoutInflater.inflate(R.layout.rv_exercise_item, parent, false);
        return new ExerciseViewHolder(v);
    }

    @Override
    public void onBindViewHolder(ExerciseViewHolder holder, int position) {
        final Exercise currentItem = exercises.get(position);
        holder.tv_name.setText(currentItem.getName());
        holder.tv_name.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (showAddEnabled)
                    showAddDialog(currentItem);
                else
                    showDialog(currentItem);
            }
        });
    }
    public void search(String searchText){
        List<Exercise> newItems = new ArrayList<>();
        for (Exercise e: exercises
             ) {
            if(e.getName().toUpperCase().contains(searchText.toUpperCase()))
                newItems.add(e);
        }
        setExercises(newItems);
    }

    private void showDialog(Exercise item) {
        // Create the fragment and show it as a dialog.
        DialogFragment newFragment = ExerciseDescriptionDialogFragment.newInstance(item);
        newFragment.show(fragmentManager, "dialog");
    }

    private void showAddDialog(Exercise exercise) {
        DialogFragment df = new AddExerciseItemDialogFragment(exercise,
                (AddExerciseItemDialogFragment.AddedExerciseItemInterface) context);
        df.show(fragmentManager, "dialogAdd");
    }

    @Override
    public int getItemCount() {
        return exercises.size();
    }

    public void setExercises(List<Exercise> exec) {
        this.exercises = exec;
        if(!showAddEnabled){
            List<Exercise> localExercises = Exercise.listAll(Exercise.class);
            for (Exercise e : localExercises) {
                exercises.add(e);
            }
        }

        Collections.sort(exercises, new NameComparator());
    }

    public void exerciseAdd(Exercise e) {
        exercises.add(e);
        Collections.sort(exercises, new NameComparator());
        notifyDataSetChanged();
    }


    public class ExerciseViewHolder extends RecyclerView.ViewHolder {
        public TextView tv_name;

        public ExerciseViewHolder(View itemView) {
            super(itemView);
            tv_name = (TextView) itemView.findViewById(R.id.rv_name_tv);
        }
    }

    public class NameComparator implements Comparator<Exercise> {
        @Override
        public int compare(Exercise o1, Exercise o2) {
            return o1.getName().compareTo(o2.getName());
        }
    }
}

