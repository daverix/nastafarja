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

    public FerriesFetcherImpl(INastaFarjaAPI api) {
        mApi = api;
    }

    private List<Departure> fetchDepartures(Ferry ferry) throws APIException {
        List<Departure> departures = new ArrayList<Departure>();
        List<String> names = ferry.getDepartsFrom();
        for(String name : names) {
            departures.add(mApi.getDeparture(ferry.getName(), name));
        }
        return departures;
    }

    @Override
    public List<Ferry> fetchFerries() throws APIException {
        Info info = mApi.getInfo();
        List<Ferry> ferries = info.getFerries();

        for(Ferry ferry : ferries) {
            ferry.setDepartures(fetchDepartures(ferry));
        }

        return ferries;
    }
}
