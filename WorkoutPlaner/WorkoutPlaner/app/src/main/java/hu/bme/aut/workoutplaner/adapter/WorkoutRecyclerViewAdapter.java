package hu.bme.aut.workoutplaner.adapter;

import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

import hu.bme.aut.workoutplaner.DailyWorkoutDescriptionActivity;
import hu.bme.aut.workoutplaner.MonthlyWorkoutDescriptionActivity;
import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.WeeklyWorkoutDescriptionActivity;
import hu.bme.aut.workoutplaner.model.DailyWorkout;
import hu.bme.aut.workoutplaner.model.MonthlyWorkout;
import hu.bme.aut.workoutplaner.model.WeeklyWorkout;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public class WorkoutRecyclerViewAdapter extends RecyclerView.Adapter<WorkoutRecyclerViewAdapter.MyViewHolder> {
    private List<DailyWorkout> dailyWorkouts;
    private List<WeeklyWorkout> weeklyWorkouts;
    private List<MonthlyWorkout> monthlyWorkouts;
    private Context context;

    public void setListener(AddDailyWorkoutInterface listener) {
        this.listener = listener;
    }

    public void setSelectable(boolean selectable) {
        this.selectable = selectable;
    }

    private AddDailyWorkoutInterface listener;

    public void setListener2(AddWeeklyWorkoutInterface listener2) {
        this.listener2 = listener2;
    }

    private AddWeeklyWorkoutInterface listener2;
    private boolean selectable = true;


    public interface AddDailyWorkoutInterface {
        void dailyWorkoutAdded(DailyWorkout dw);
    }
    public interface AddWeeklyWorkoutInterface {
        void weeklyWorkoutAdded(WeeklyWorkout ww);
    }
    public WorkoutRecyclerViewAdapter(Context _context) {
        this.context = _context;
    }

    public WorkoutRecyclerViewAdapter(AddDailyWorkoutInterface listener, boolean selectable, Context context) {
        this.listener = listener;
        this.selectable = selectable;
        this.context = context;
    }


    public List<DailyWorkout> getDailyWorkouts() {
        return dailyWorkouts;
    }

    public void setDailyWorkouts(List<DailyWorkout> dailyWorkouts) {
        this.dailyWorkouts = dailyWorkouts;
        notifyDataSetChanged();
    }

    public List<WeeklyWorkout> getWeeklyWorkouts() {
        return weeklyWorkouts;
    }

    public void setWeeklyWorkouts(List<WeeklyWorkout> weeklyWorkouts) {
        this.weeklyWorkouts = weeklyWorkouts;
        notifyDataSetChanged();
    }

    public List<MonthlyWorkout> getMonthlyWorkouts() {
        return monthlyWorkouts;
    }

    public void setMonthlyWorkouts(List<MonthlyWorkout> monthlyWorkouts) {
        this.monthlyWorkouts = monthlyWorkouts;
        notifyDataSetChanged();
    }

    @Override
    public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.rv_workout_item, parent, false);
        return new MyViewHolder(view);
    }


    @Override
    public void onBindViewHolder(MyViewHolder holder, final int position) {
        if (dailyWorkouts != null) {
            holder.tv.setText(dailyWorkouts.get(position).getName());
            if (selectable) holder.tv.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent intent = new Intent(context, DailyWorkoutDescriptionActivity.class);
                    intent.putExtra("id", dailyWorkouts.get(position).getId());
                    context.startActivity(intent);
                }
            });
            else{
                holder.tv.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        listener.dailyWorkoutAdded(dailyWorkouts.get(position));
                    }
                });
            }

        }

        if (weeklyWorkouts != null) {
            holder.tv.setText(weeklyWorkouts.get(position).getName());
            if(selectable)
            holder.tv.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent intent = new Intent(context, WeeklyWorkoutDescriptionActivity.class);
                    intent.putExtra("id", weeklyWorkouts.get(position).getId());
                    context.startActivity(intent);
                }
            });
            else holder.tv.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    listener2.weeklyWorkoutAdded(weeklyWorkouts.get(position));
                }
            });
        }

        if (monthlyWorkouts != null) {
            holder.tv.setText(monthlyWorkouts.get(position).getName());
            holder.tv.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent intent = new Intent(context, MonthlyWorkoutDescriptionActivity.class);
                    intent.putExtra("id", monthlyWorkouts.get(position).getId());
                    context.startActivity(intent);
                }
            });
        }
    }

    @Override
    public int getItemCount() {
        if (dailyWorkouts != null)
            return dailyWorkouts.size();
        if (weeklyWorkouts != null)
            return weeklyWorkouts.size();
        if (monthlyWorkouts != null)
            return monthlyWorkouts.size();
        return 0;
    }


    public class MyViewHolder extends RecyclerView.ViewHolder {
        TextView tv;

        public MyViewHolder(View itemView) {
            super(itemView);
            tv = (TextView) itemView.findViewById(R.id.tv_workout_name);
        }
    }
}
