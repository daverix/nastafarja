package net.daverix.nastafarja.view;

import android.content.Context;
import android.text.TextUtils;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.RelativeLayout;
import android.widget.TextView;

import net.daverix.nastafarja.R;

import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public class DepartureViewFactory implements FerryListViewFactory {
    private Context mContext;

    public DepartureViewFactory(Context context) {
        mContext = context;
    }

    @Override
    public View create(View convertView, FerryListRow row) {
        DepartureViewHolder holder;
        if(convertView == null) {
            convertView = LayoutInflater.from(mContext).inflate(R.layout.departure_row, null);
            holder = new DepartureViewHolder();
            holder.title = (TextView) convertView.findViewById(R.id.textTitle);
            holder.times = (TextView) convertView.findViewById(R.id.times);
            convertView.setTag(holder);
        }
        else {
            holder = (DepartureViewHolder) convertView.getTag();
        }

        holder.title.setText(((DepartureRow)row).getTitle());
        List<String> times = ((DepartureRow)row).getTimes();
        if(times != null) {
            holder.times.setText(TextUtils.join(" ", times));
        }

        return convertView;
    }

    private static class DepartureViewHolder {
        TextView title;
        TextView times;
    }
}
