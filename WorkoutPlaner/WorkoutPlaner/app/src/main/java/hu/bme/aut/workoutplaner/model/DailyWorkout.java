package hu.bme.aut.workoutplaner.model;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public class DailyWorkout extends BaseWorkout {

    public DailyWorkout(int id, String name, Type workoutType) {
        super(id, name, workoutType);
        exercises = new ArrayList<>();
    }

    public DailyWorkout(int id, String name, Type workoutType, List<ExerciseItem> exerciseItems) {
        super(id, name, workoutType);
        this.exercises = exerciseItems;
    }

    private List<ExerciseItem> exercises;

    public List<ExerciseItem> getExercises() {
        return exercises;
    }

    public void setExercises(List<ExerciseItem> exercises) {
        this.exercises = exercises;
    }
}
