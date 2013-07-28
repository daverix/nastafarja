package net.daverix.nastafarja.model;

import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by daverix on 7/26/13.
 */
public class Ferry {
    @SerializedName(value = "Name")        private String mName;
    @SerializedName(value = "Region")      private String mRegion;
    @SerializedName(value = "Url")         private String mUrl;
    @SerializedName(value = "DepartsFrom") private List<String> mDepartsFrom;
    private transient List<Departure> mDepartures;

    public Ferry() {

    }

    public String getName() {
        return mName;
    }

    public String getRegion() {
        return mRegion;
    }

    public String getUrl() {
        return mUrl;
    }

    public List<String> getDepartsFrom() {
        return mDepartsFrom;
    }

    public List<Departure> getDepartures() {
        return mDepartures;
    }

    public void setDepartures(List<Departure> departures) {
        mDepartures = departures;
    }
}
