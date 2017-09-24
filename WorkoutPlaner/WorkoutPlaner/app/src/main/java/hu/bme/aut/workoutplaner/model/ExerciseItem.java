package hu.bme.aut.workoutplaner.model;

/**
 * Created by Balazs on 2017. 04. 21..
 */

public class ExerciseItem extends Exercise {


    private int serialNumber;
    private int reps;

    public ExerciseItem(String name, String description, String muscleGroup, int serialNumber, int reps) {
        super(name, description, muscleGroup);
        this.serialNumber = serialNumber;
        this.reps = reps;
    }

    public ExerciseItem(String name, String description, String muscleGroup) {
        super(name, description, muscleGroup);
    }


    public int getSerialNumber() {
        return serialNumber;
    }

    public void setSerialNumber(int serialNumber) {
        this.serialNumber = serialNumber;
    }

    public int getReps() {
        return reps;
    }

    public void setReps(int reps) {
        this.reps = reps;
    }
}
