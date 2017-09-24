package hu.bme.aut.workoutplaner.adapter;

import android.content.Context;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentStatePagerAdapter;

import java.util.ArrayList;
import java.util.List;

import hu.bme.aut.workoutplaner.fragment.DailyWorkoutFragment;
import hu.bme.aut.workoutplaner.fragment.MonthlyWorkoutFragment;
import hu.bme.aut.workoutplaner.fragment.WeeklyWorkoutFragment;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public class WorkoutPagerAdapter extends FragmentStatePagerAdapter {
    List<Fragment> myFragments = new ArrayList<>();
    public WorkoutPagerAdapter(FragmentManager fm, Context context) {
        super(fm);
        myFragments.add(new DailyWorkoutFragment(context));
        myFragments.add(new WeeklyWorkoutFragment(context));
        myFragments.add(new MonthlyWorkoutFragment(context));
    }

    @Override
    public Fragment getItem(int position) {
        return  myFragments.get(position);
    }

    @Override
    public int getCount() {
        return 3;
    }
    @Override
    public CharSequence getPageTitle(int position) {
        switch (position) {
            case 0:
                return "Napi";
            case 1:
                return "Heti";
            case 2:
                return "Havi";

            default:
                throw new IllegalArgumentException();
        }
    }
}
