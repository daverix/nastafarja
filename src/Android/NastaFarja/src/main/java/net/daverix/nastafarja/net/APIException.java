package net.daverix.nastafarja.net;

/**
 * Created by daverix on 7/28/13.
 */
public class APIException extends Exception {
    public APIException(String message, Throwable e) {
        super(message, e);
    }
}
