package hu.bme.aut.workoutplaner.fragment;


import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatDialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.DatePicker;
import android.widget.TimePicker;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.service.CalendarService;

public class CalendarDateTimeDialogFragment extends AppCompatDialogFragment {

    DatePicker datePicker;
    TimePicker timePicker;
    String nameOfExercise;
    Context context;
    public CalendarDateTimeDialogFragment(Context c, String name) {
        this.context = c;
        this.nameOfExercise = name;
    }

    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        return new AlertDialog.Builder(context)
                .setView(initView())
                .setTitle("Add meg az időpontot")
                .setNegativeButton(getString(R.string.cancel), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dismiss();
                    }
                })
                .setPositiveButton(getString(R.string.save), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        CalendarService service = new CalendarService(context);
                        service.saveEventToCalendar(nameOfExercise,
                                datePicker.getYear(),datePicker.getMonth(),datePicker.getDayOfMonth(),
                                timePicker.getCurrentHour(),timePicker.getCurrentMinute());
                    }
                })
                .create();
    }

    private  View initView(){
        View v = LayoutInflater.from(context).inflate(R.layout.fragment_calendar_date_time_dialog,null);
        datePicker = (DatePicker) v.findViewById(R.id.datePicker1);

        timePicker = (TimePicker) v.findViewById(R.id.timePicker1);
        timePicker.setIs24HourView(true);

        return v;
    }

}
