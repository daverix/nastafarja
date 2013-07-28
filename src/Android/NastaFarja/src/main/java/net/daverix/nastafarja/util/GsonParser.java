package net.daverix.nastafarja.util;

import com.google.gson.Gson;

/**
 * Created by daverix on 7/28/13.
 */
public class GsonParser implements Parser {
    private Gson mGson;

    public GsonParser() {
        mGson = new Gson();
    }
    
    @Override
    public <T> T parse(String data, Class<T> clazz) throws ParseException {
        try {
            return mGson.fromJson(data, clazz);
        } catch (Exception e) {
            throw new ParseException(e);
        }
    }
}
