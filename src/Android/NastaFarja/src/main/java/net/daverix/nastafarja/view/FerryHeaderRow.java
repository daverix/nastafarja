package net.daverix.nastafarja.view;

/**
 * Created by daverix on 7/28/13.
 */
public class FerryHeaderRow implements FerryListRow {
    private String mTitle;

    public FerryHeaderRow(String title) {
        mTitle = title;
    }

    @Override
    public int getRowType() {
        return TYPE_FERRY;
    }

    public String getTitle() {
        return mTitle;
    }
}
