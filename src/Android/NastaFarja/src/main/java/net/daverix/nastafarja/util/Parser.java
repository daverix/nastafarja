package net.daverix.nastafarja.util;

/**
 * Created by daverix on 7/26/13.
 */
public interface Parser {
    public <T> T parse(String data, Class<T> clazz) throws ParseException;
}
