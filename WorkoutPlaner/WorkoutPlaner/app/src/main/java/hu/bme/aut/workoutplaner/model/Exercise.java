package hu.bme.aut.workoutplaner.model;

import com.orm.SugarRecord;

/**
 * Created by Balazs on 2017. 04. 21..
 */

public class Exercise extends SugarRecord {


    protected String name;
    protected String description;
    protected String muscleGroup;
    private String imagePath;

    public Exercise() {
    }

    public Exercise(String name, String description, String muscleGroup) {
        this.name = name;
        this.description = description;
        this.muscleGroup = muscleGroup;
    }

    public String getImagePath() {
        return imagePath;
    }

    public void setImagePath(String imagePath) {
        this.imagePath = imagePath;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getMuscleGroup() {
        return muscleGroup;
    }

    public void setMuscleGroup(String muscleGroup) {
        this.muscleGroup = muscleGroup;
    }

}
