package net.daverix.nastafarja.model;

import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by daverix on 7/27/13.
 */
public class Departure {
    @SerializedName(value = "Title")           private String mTitle;
    @SerializedName(value = "ArrivesAt")       private String mArrivesAt;
    @SerializedName(value = "DepartsFrom")     private String mDepartsFrom;
    @SerializedName(value = "Name")            private String mName;
    @SerializedName(value = "NextDepartures")  private List<DepartureTime> mNextDepartures;

    public Departure() {

    }

    public String getArrivesAt() {
        return mArrivesAt;
    }

    public String getDepartsFrom() {
        return mDepartsFrom;
    }

    public String getName() {
        return mName;
    }

    public List<DepartureTime> getNextDepartures() {
        return mNextDepartures;
    }

    public String getTitle() {
        return mTitle;
    }
}
