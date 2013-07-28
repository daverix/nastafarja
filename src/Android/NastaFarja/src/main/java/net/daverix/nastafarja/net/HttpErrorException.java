package net.daverix.nastafarja.net;

/**
 * Created by daverix on 7/26/13.
 */
public class HttpErrorException extends Exception {
    private int mErrorCode;

    public HttpErrorException(String message, int errorCode) {
        super(message);

        mErrorCode = errorCode;
    }

    public HttpErrorException(String message, Throwable e) {
        super(message, e);
    }
}
