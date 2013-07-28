package net.daverix.nastafarja.util;

/**
 * Created by daverix on 7/26/13.
 */
public class ParseException extends Exception {
    public ParseException(String message) {
        super(message);
    }

    public ParseException(Exception e) {
        super(e);
    }

    public ParseException(String message, Throwable e) {
        super(message, e);
    }
}
