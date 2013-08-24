package net.daverix.nastafarja.net;

import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.Ferry;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by daverix on 8/24/13.
 */
public class DepartureFetcherImpl implements DepartureFetcher {
    private final INastaFarjaAPI mApi;

    public DepartureFetcherImpl(INastaFarjaAPI api) {
        mApi = api;
    }

    @Override
    public List<Departure> fetchDepartures(Ferry ferry) throws APIException {
        List<Departure> departures = new ArrayList<Departure>();
        List<String> names = ferry.getDepartsFrom();
        for(String name : names) {
            departures.add(mApi.getDeparture(ferry.getName(), name));
        }
        return departures;
    }
}
