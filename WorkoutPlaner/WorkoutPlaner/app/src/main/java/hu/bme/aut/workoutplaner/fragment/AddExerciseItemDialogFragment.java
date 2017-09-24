package hu.bme.aut.workoutplaner.fragment;

import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatDialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.model.Exercise;
import hu.bme.aut.workoutplaner.model.ExerciseItem;

/**
 * Created by Balazs on 2017. 04. 24..
 */

public class AddExerciseItemDialogFragment extends AppCompatDialogFragment {
    private Exercise exercise;
    EditText serial;
    EditText reps;
    private AddedExerciseItemInterface listener;

    public AddExerciseItemDialogFragment(Exercise exercise, AddedExerciseItemInterface listener) {
        this.exercise = exercise;
        this.listener = listener;

    }

    public interface AddedExerciseItemInterface {
        void AddedExerciseItem(ExerciseItem item,boolean isEdited,int position);
    }

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        return new AlertDialog.Builder(getContext())
                .setView(initView())
                .setTitle("Új gyakorlat elem hozzáadása")
                .setPositiveButton(R.string.save, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        if (!serial.getText().toString().equals("") &&
                                !reps.getText().toString().equals("")){
                            if (getArguments()!=null)
                            listener.AddedExerciseItem(new ExerciseItem(
                                    exercise.getName(), exercise.getDescription(), exercise.getMuscleGroup(),
                                    Integer.parseInt(serial.getText().toString()),
                                    Integer.parseInt(reps.getText().toString())),true,getArguments().getInt("pos"));
                            else listener.AddedExerciseItem(new ExerciseItem(
                                    exercise.getName(), exercise.getDescription(), exercise.getMuscleGroup(),
                                    Integer.parseInt(serial.getText().toString()),
                                    Integer.parseInt(reps.getText().toString())),false,-1);
                        }

                    }
                })
                .setNegativeButton(R.string.cancel, new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog, int which) {
                                dismiss();
                            }
                        })
                .create();
    }

    private View initView() {
        View view = LayoutInflater.from(getContext()).inflate(R.layout.dialog_fragment_exercise_item, null);
        serial = (EditText) view.findViewById(R.id.et_exercise_item_serial);
        reps = (EditText) view.findViewById(R.id.et_exercise_item_reps);
        if(getArguments()!=null){
            Bundle b = getArguments();
            serial.setText(b.getString("serial"));
            reps.setText(b.getString("reps"));
        }
        return view;

    }
}
