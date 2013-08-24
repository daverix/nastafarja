package net.daverix.nastafarja.net;

import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.Ferry;

import java.util.List;

/**
 * Created by daverix on 8/24/13.
 */
public interface DepartureFetcher {
    public List<Departure> fetchDepartures(Ferry ferry) throws APIException;
}
