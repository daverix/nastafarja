package net.daverix.nastafarja.net;

import net.daverix.nastafarja.model.Ferry;

import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public interface FerriesFetcher {
    public List<Ferry> fetchFerries() throws APIException;
}
