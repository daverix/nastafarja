package net.daverix.nastafarja.view;

import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public class DepartureRow implements FerryListRow {
    private String mTitle;
    private List<String> mTimes;

    public DepartureRow(String title, List<String> times) {
        mTitle = title;
        mTimes = times;
    }

    @Override
    public int getRowType() {
        return TYPE_DEPARTURE;
    }

    public String getTitle() {
        return mTitle;
    }

    public List<String> getTimes() {
        return mTimes;
    }
}
