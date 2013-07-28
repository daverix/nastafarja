package net.daverix.nastafarja.model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by daverix on 7/27/13.
 */
public class DepartureTime {
    @SerializedName(value = "TimeOfDay") private String mTimeOfDay;

    public DepartureTime() {

    }

    public String getTimeOfDay() {
        return mTimeOfDay;
    }

    public void setTimeOfDay(String timeOfDay) {
        mTimeOfDay = timeOfDay;
    }
}
