package hu.bme.aut.workoutplaner.model;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public class WeeklyWorkout extends BaseWorkout {

    public WeeklyWorkout(int id, String name, Type workoutType) {
        super(id, name, workoutType);
    }

    public WeeklyWorkout(int id, String name, Type workoutType, DailyWorkout dayOne, DailyWorkout dayTwo, DailyWorkout dayThree, DailyWorkout dayFour, DailyWorkout dayFive, DailyWorkout daySix, DailyWorkout daySeven) {
        super(id, name, workoutType);
        this.dayOne = dayOne;
        this.dayTwo = dayTwo;
        this.dayThree = dayThree;
        this.dayFour = dayFour;
        this.dayFive = dayFive;
        this.daySix = daySix;
        this.daySeven = daySeven;
    }

    private DailyWorkout dayOne;
    private DailyWorkout dayTwo;
    private DailyWorkout dayThree;
    private DailyWorkout dayFour;
    private DailyWorkout dayFive;
    private DailyWorkout daySix;
    private DailyWorkout daySeven;

    public DailyWorkout getDayOne() {
        return dayOne;
    }

    public void setDayOne(DailyWorkout dayOne) {
        this.dayOne = dayOne;
    }

    public DailyWorkout getDayTwo() {
        return dayTwo;
    }

    public void setDayTwo(DailyWorkout dayTwo) {
        this.dayTwo = dayTwo;
    }

    public DailyWorkout getDayThree() {
        return dayThree;
    }

    public void setDayThree(DailyWorkout dayThree) {
        this.dayThree = dayThree;
    }

    public DailyWorkout getDayFour() {
        return dayFour;
    }

    public void setDayFour(DailyWorkout dayFour) {
        this.dayFour = dayFour;
    }

    public DailyWorkout getDayFive() {
        return dayFive;
    }

    public void setDayFive(DailyWorkout dayFive) {
        this.dayFive = dayFive;
    }

    public DailyWorkout getDaySix() {
        return daySix;
    }

    public void setDaySix(DailyWorkout daySix) {
        this.daySix = daySix;
    }

    public DailyWorkout getDaySeven() {
        return daySeven;
    }

    public void setDaySeven(DailyWorkout daySeven) {
        this.daySeven = daySeven;
    }
}
