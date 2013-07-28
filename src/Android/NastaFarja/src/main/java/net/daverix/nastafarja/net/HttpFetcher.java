package net.daverix.nastafarja.net;

/**
 * Created by daverix on 7/26/13.
 */
public interface HttpFetcher {
    public String fetch(String host, String file) throws HttpErrorException;
}
