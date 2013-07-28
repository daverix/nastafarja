package net.daverix.nastafarja.net;

import net.daverix.nastafarja.util.Parser;
import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.Ferry;
import net.daverix.nastafarja.model.Info;

import java.net.URLEncoder;
import java.util.List;

/**
 * Created by daverix on 7/26/13.
 */
public class NastaFarjaAPI implements INastaFarjaAPI {
    private static final String API_PREFIX = "/api/1.0/";
    private final String mHost;
    private final Parser mParser;
    private final HttpFetcher mHttpFetcher;

    public NastaFarjaAPI(String host, Parser parser, HttpFetcher httpFetcher) {
        mHost = host;
        mParser = parser;
        mHttpFetcher = httpFetcher;
    }

    @Override
    public Info getInfo() throws APIException {
        try {
            String data = mHttpFetcher.fetch(mHost, API_PREFIX + "info");
            return mParser.parse(data, Info.class);
        } catch (Exception e) {
            throw new APIException("Error getting ferries", e);
        }
    }

    @Override
    public Departure getDeparture(String infoName, String departsFrom) throws APIException {
        try {
            String data = mHttpFetcher.fetch(mHost, API_PREFIX + "nextDeparture/"
                    + URLEncoder.encode(infoName, "UTF-8").replace("+", "%20") + "/"
                    + URLEncoder.encode(departsFrom, "UTF-8").replace("+", "%20"));

            return mParser.parse(data, Departure.class);
        } catch (Exception e) {
            throw new APIException("Error getting departure for " + infoName + " from " + departsFrom, e);
        }
    }
}
