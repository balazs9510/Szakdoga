package hu.bme.aut.workoutplaner.model;

/**
 * Created by Balazs on 2017. 04. 23..
 */

public class MonthlyWorkout extends BaseWorkout {

    public MonthlyWorkout(int id, String name, Type workoutType) {
        super(id, name, workoutType);
    }

    public MonthlyWorkout(int id, String name, Type workoutType, WeeklyWorkout weekOne, WeeklyWorkout weekTwo, WeeklyWorkout weekThree, WeeklyWorkout weekFour) {
        super(id, name, workoutType);
        this.weekOne = weekOne;
        this.weekTwo = weekTwo;
        this.weekThree = weekThree;
        this.weekFour = weekFour;
    }

    public WeeklyWorkout weekOne;
    public WeeklyWorkout weekTwo;
    public WeeklyWorkout weekThree;
    public WeeklyWorkout weekFour;

    public WeeklyWorkout getWeekOne() {
        return weekOne;
    }

    public void setWeekOne(WeeklyWorkout weekOne) {
        this.weekOne = weekOne;
    }

    public WeeklyWorkout getWeekTwo() {
        return weekTwo;
    }

    public void setWeekTwo(WeeklyWorkout weekTwo) {
        this.weekTwo = weekTwo;
    }

    public WeeklyWorkout getWeekThree() {
        return weekThree;
    }

    public void setWeekThree(WeeklyWorkout weekThree) {
        this.weekThree = weekThree;
    }

    public WeeklyWorkout getWeekFour() {
        return weekFour;
    }

    public void setWeekFour(WeeklyWorkout weekFour) {
        this.weekFour = weekFour;
    }
}
