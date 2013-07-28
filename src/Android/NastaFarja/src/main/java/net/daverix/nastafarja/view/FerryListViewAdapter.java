package net.daverix.nastafarja.view;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public class FerryListViewAdapter extends BaseAdapter {
    private final List<FerryListRow> mRows;
    private final FerryListViewFactory mFerryViewFactory;
    private final FerryListViewFactory mDepartureViewFactory;

    public FerryListViewAdapter(List<FerryListRow> rows,
                                FerryListViewFactory ferryViewFactory,
                                FerryListViewFactory departureViewFactory) {
        super();
        mRows = rows;
        mFerryViewFactory = ferryViewFactory;
        mDepartureViewFactory = departureViewFactory;
    }

    @Override
    public int getCount() {
        return mRows.size();
    }

    @Override
    public FerryListRow getItem(int position) {
        return mRows.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public int getItemViewType(int position) {
        return getItem(position).getRowType();
    }

    @Override
    public int getViewTypeCount() {
        return 2;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        int type = getItemViewType(position);
        switch (type) {
            case FerryListRow.TYPE_FERRY:
                return mFerryViewFactory.create(convertView, getItem(position));
            case FerryListRow.TYPE_DEPARTURE:
                return mDepartureViewFactory.create(convertView, getItem(position));
            default:
                return null;
        }
    }
}
