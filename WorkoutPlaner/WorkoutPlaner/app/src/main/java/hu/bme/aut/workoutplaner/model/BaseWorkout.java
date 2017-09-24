package hu.bme.aut.workoutplaner.model;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public abstract class BaseWorkout {
    public enum Type
    {
        Daily,
        Weekly,
        Monthly
    }
    protected int id ;
    protected String name ;
    protected Type workoutType;

    public BaseWorkout(int id, String name, Type workoutType) {
        this.id = id;
        this.name = name;
        this.workoutType = workoutType;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Type getWorkoutType() {
        return workoutType;
    }

    public void setWorkoutType(Type workoutType) {
        this.workoutType = workoutType;
    }
}
