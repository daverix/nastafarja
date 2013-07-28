package net.daverix.nastafarja.view;

import android.view.View;

/**
 * Created by daverix on 7/28/13.
 */
public interface FerryListViewFactory {
    public View create(View convertView, FerryListRow row);
}
