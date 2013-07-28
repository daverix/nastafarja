package net.daverix.nastafarja.net;

import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.Ferry;
import net.daverix.nastafarja.model.Info;

import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public interface INastaFarjaAPI {
    public Info getInfo() throws APIException;

    public Departure getDeparture(String infoName, String departsFrom) throws APIException;
}
