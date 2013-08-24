package net.daverix.nastafarja.net;

import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.Ferry;
import net.daverix.nastafarja.model.Info;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public class FerriesFetcherImpl implements FerriesFetcher {
    private final INastaFarjaAPI mApi;
    private final DepartureFetcher mDepartureFetcher;

    public FerriesFetcherImpl(INastaFarjaAPI api, DepartureFetcher departureFetcher) {
        mApi = api;
        mDepartureFetcher = departureFetcher;
    }

    @Override
    public List<Ferry> fetchFerries() throws APIException {
        Info info = mApi.getInfo();
        List<Ferry> ferries = info.getFerries();

        for(Ferry ferry : ferries) {
            ferry.setDepartures(mDepartureFetcher.fetchDepartures(ferry));
        }

        return ferries;
    }
}
