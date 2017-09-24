package hu.bme.aut.workoutplaner.service;

import android.content.Context;
import android.content.Intent;
import android.provider.CalendarContract;

import java.util.Calendar;
import java.util.Date;

/**
 * Created by Balazs on 2017. 04. 24..
 */

public class CalendarService {
    private Context context;

    public CalendarService(Context context) {
        this.context = context;
    }
    public void saveEventToCalendar(String title,int year,int month,int day, int hour, int min){
        Calendar calendar = Calendar.getInstance();
        calendar.set(Calendar.YEAR,year);
        calendar.set(Calendar.MONTH, month-1);
        calendar.set(Calendar.DAY_OF_MONTH, day);
        calendar.set(Calendar.HOUR_OF_DAY, hour);
        calendar.set(Calendar.MINUTE, min);
        Date date = new Date(year,month,day,hour,min);
        Intent intent = new Intent(Intent.ACTION_INSERT)
                .setData(CalendarContract.Events.CONTENT_URI)
                .putExtra(CalendarContract.EXTRA_EVENT_BEGIN_TIME, calendar.getTimeInMillis())
                .putExtra(CalendarContract.EXTRA_EVENT_END_TIME, date.getTime()+60*60*1000)
                .putExtra(CalendarContract.Events.TITLE, title);

        context.startActivity(intent);
    }
}
