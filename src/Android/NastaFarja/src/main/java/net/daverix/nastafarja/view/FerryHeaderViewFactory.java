package net.daverix.nastafarja.view;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.TextView;

import net.daverix.nastafarja.R;

/**
 * Created by daverix on 7/28/13.
 */
public class FerryHeaderViewFactory implements FerryListViewFactory {
    private final Context mContext;

    public FerryHeaderViewFactory(Context context) {
        mContext = context;
    }

    @Override
    public View create(View convertView, FerryListRow row) {
        FerryViewHolder holder;
        if(convertView == null) {
            convertView = LayoutInflater.from(mContext).inflate(R.layout.ferry_title_row, null);
            holder = new FerryViewHolder();
            holder.title = (TextView) convertView.findViewById(R.id.textTitle);
            convertView.setTag(holder);
        }
        else {
            holder = (FerryViewHolder) convertView.getTag();
        }

        holder.title.setText(((FerryHeaderRow) row).getTitle());

        return convertView;
    }

    private static class FerryViewHolder {
        TextView title;
    }
}
