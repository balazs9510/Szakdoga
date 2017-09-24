package hu.bme.aut.workoutplaner;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;

import java.util.Date;

import butterknife.BindView;
import butterknife.ButterKnife;
import hu.bme.aut.workoutplaner.adapter.WorkoutPagerAdapter;


public class MainActivity extends AppCompatActivity {
    private static final String KEY_START = "START";
    @BindView(R.id.pager)
    ViewPager mViewPager;
    @BindView(R.id.toolbar)
    Toolbar toolbar;
    @BindView(R.id.tabs)
    TabLayout tabLayout;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        ButterKnife.bind(this);
        setSupportActionBar(toolbar);
        mViewPager.setAdapter(new WorkoutPagerAdapter(getSupportFragmentManager(), this));
        tabLayout.setupWithViewPager(mViewPager);
       /* CalendarService service = new CalendarService(this);
        service.saveEventToCalendar("ASD",2017,04,24,22,22);*/
        //Todo demózás
       /* DialogFragment df = new CalendarDateTimeDialogFragment(this,"Hali");
        df.show(getSupportFragmentManager(),"halika");*/
        if (showLastStart().equals("NEVER")) {
            new AlertDialog.Builder(this)
                    .setTitle("Üdvözöllek")
                    .setMessage("Úgy látom most indítottad el először az appot." +
                            " Remélem élvezni fogod a használatát. :) ")
                    .setNegativeButton(getString(R.string.close), new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            dialog.dismiss();
                        }
                    })
                    .create().show();

        }
        saveLastStart();
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    protected void onResume() {
        super.onResume();
        mViewPager.setAdapter(new WorkoutPagerAdapter(getSupportFragmentManager(), this));
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_exercises) {
            Intent intent = new Intent(this, ExercisesActivity.class);
            startActivity(intent);
        }
        if (id == R.id.action_add_daily_workout) {
            Intent intent = new Intent(this, AddDailyWorkoutActivity.class);
            startActivity(intent);
        }
        if (id == R.id.action_add_weekly_workout) {
            Intent intent = new Intent(this, AddWeeklyWorkoutActivity.class);
            startActivity(intent);
        }
        if (id == R.id.action_add_monthly_workout) {
            Intent intent = new Intent(this, AddMonthlyWorkoutActivity.class);
            startActivity(intent);
        }

        return super.onOptionsItemSelected(item);
    }

    private void saveLastStart() {
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(this);

        SharedPreferences.Editor edit = sp.edit();
        edit.putString(KEY_START, new Date(System.currentTimeMillis()).toString());
        edit.commit();
    }

    private String showLastStart() {
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(this);
        String start = sp.getString(KEY_START, "NEVER");
        return start;
    }


}
