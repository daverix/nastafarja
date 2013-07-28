package net.daverix.nastafarja;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import net.daverix.nastafarja.model.Ferry;
import net.daverix.nastafarja.net.APIException;
import net.daverix.nastafarja.net.FerriesFetcher;
import net.daverix.nastafarja.net.FerriesFetcherImpl;
import net.daverix.nastafarja.net.HttpFetcher;
import net.daverix.nastafarja.net.INastaFarjaAPI;
import net.daverix.nastafarja.net.NastaFarjaAPI;
import net.daverix.nastafarja.net.SimpleHttpFetcher;
import net.daverix.nastafarja.util.GsonParser;
import net.daverix.nastafarja.util.Parser;
import net.daverix.nastafarja.view.DepartureViewFactory;
import net.daverix.nastafarja.view.FerryHeaderViewFactory;
import net.daverix.nastafarja.view.FerryListRow;
import net.daverix.nastafarja.view.FerryListRowFactory;
import net.daverix.nastafarja.view.FerryListRowFactoryImpl;
import net.daverix.nastafarja.view.FerryListViewAdapter;
import net.daverix.nastafarja.view.FerryListViewFactory;

import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public class FerriesListFragment extends Fragment {
    private static final String API_HOST = "nastafarja.se";
    private ListView mListView;

    private HttpFetcher mHttpFetcher;
    private Parser mParser;
    private INastaFarjaAPI mApi;
    private FerriesFetcher mFerriesFetcher;
    private FerryListRowFactory mRowsFactory;
    private FerryListViewFactory mFerryHeaderFactory;
    private FerryListViewFactory mDepartureRowFactory;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        mHttpFetcher = new SimpleHttpFetcher();
        mParser = new GsonParser();
        mApi = new NastaFarjaAPI(API_HOST, mParser, mHttpFetcher);
        mFerriesFetcher = new FerriesFetcherImpl(mApi);
        mFerryHeaderFactory = new FerryHeaderViewFactory(getActivity());
        mDepartureRowFactory = new DepartureViewFactory(getActivity());
        mRowsFactory = new FerryListRowFactoryImpl();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_ferries, null);
        mListView = (ListView) v.findViewById(R.id.listFerries);
        return v;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);

        new LoadFerriesTask().execute();
    }

    private class LoadFerriesTask extends AsyncTask<Void, Void, List<Ferry>> {
        @Override
        protected List<Ferry> doInBackground(Void... params) {
            try {
                return mFerriesFetcher.fetchFerries();
            } catch (APIException e) {
                Log.e("FerriesListFragment", "Error getting ferries", e);
                return null;
            }
        }

        @Override
        protected void onPostExecute(List<Ferry> ferries) {
            super.onPostExecute(ferries);

            if(ferries != null) {
                List<FerryListRow> rows = mRowsFactory.createRows(ferries);
                mListView.setAdapter(new FerryListViewAdapter(rows, mFerryHeaderFactory, mDepartureRowFactory));
            }
        }
    }
}
