package hu.bme.aut.workoutplaner.adapter;

import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.FragmentManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

import hu.bme.aut.workoutplaner.R;
import hu.bme.aut.workoutplaner.fragment.AddExerciseItemDialogFragment;
import hu.bme.aut.workoutplaner.model.ExerciseItem;

/**
 * Created by Balazs on 2017. 04. 24..
 */

public class ExerciseItemAdapter extends RecyclerView.Adapter<ExerciseItemAdapter.ExerciseItemViewHolder>{
    private Context context;
    private FragmentManager fragmentManager;
    private List<ExerciseItem> exerciseItems;
    private boolean isEditable = true;
    public ExerciseItemAdapter(Context context,FragmentManager fm, boolean edit) {
        this.context = context;
        exerciseItems = new ArrayList<>();
        fragmentManager = fm;
        isEditable = edit;
    }

    public List<ExerciseItem> getExerciseItems() {
        return exerciseItems;
    }

    public void setExerciseItems(List<ExerciseItem> exerciseItems) {
        this.exerciseItems = exerciseItems;
    }

    @Override
    public ExerciseItemViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(context).inflate(R.layout.rv_exerxiseitem_item,parent,false);
        return new ExerciseItemViewHolder(view);
    }

    @Override
    public void onBindViewHolder(ExerciseItemViewHolder holder, final int position) {
        final ExerciseItem current = exerciseItems.get(position);
        holder.tv_name.setText(current.getName());
        holder.tv_serial.setText(String.valueOf(current.getSerialNumber())+ " X ");
        holder.tv_reps.setText(String .valueOf(current.getReps()));
        if(isEditable){
            holder.btn_delete.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    exerciseItems.remove(position);
                    notifyDataSetChanged();
                }
            });
            holder.btn_edit.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    showAddDialog(current,position);
                }
            });
        }else{
            holder.btn_edit.setVisibility(View.INVISIBLE);
            holder.btn_delete.setVisibility(View.INVISIBLE);
        }

    }
    private void showAddDialog(ExerciseItem exercise,int position){
        DialogFragment df = new AddExerciseItemDialogFragment(exercise,
                (AddExerciseItemDialogFragment.AddedExerciseItemInterface) context);
        Bundle bundle = new Bundle();
        bundle.putString("serial",String.valueOf(exercise.getSerialNumber()));
        bundle.putString("reps",String.valueOf(exercise.getReps()));
        bundle.putInt("pos",position);
        df.setArguments(bundle);
        df.show(fragmentManager,"dialogAdd");
    }
    public void addExecriseItem(ExerciseItem item){
        exerciseItems.add(item);
        notifyDataSetChanged();
    }
    public  void removeExerciseItem(int position){
        exerciseItems.remove(position);
        notifyDataSetChanged();
        notifyDataSetChanged();
    }

    @Override
    public int getItemCount() {
        return exerciseItems.size();
    }

    public class ExerciseItemViewHolder extends RecyclerView.ViewHolder{
        TextView tv_name;
        TextView tv_serial;
        TextView tv_reps;
        Button btn_delete;
        Button btn_edit;
        public ExerciseItemViewHolder(View itemView) {
            super(itemView);
            tv_name = (TextView) itemView.findViewById(R.id.tv_exerciseitem_name);
            tv_serial = (TextView) itemView.findViewById(R.id.tv_exerciseitem_serial);
            tv_reps = (TextView) itemView.findViewById(R.id.tv_exerciseitem_reps);
            btn_delete = (Button) itemView.findViewById(R.id.btn_delete_exerciseitem);
            btn_edit = (Button) itemView.findViewById(R.id.btn_edit_exercise_item);
        }
    }
}
