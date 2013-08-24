package net.daverix.nastafarja.net;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

/**
 * Created by daverix on 7/26/13.
 */
public class SimpleHttpFetcher implements HttpFetcher {

    public SimpleHttpFetcher() {

    }

    @Override
    public String fetch(String host, String file) throws HttpErrorException {
        HttpURLConnection urlConnection = null;
        try {
            URL url = new URL("http", host, file);
            urlConnection = (HttpURLConnection) url.openConnection();
            final int responseCode = urlConnection.getResponseCode();
            if(responseCode != 200) {
                throw new HttpErrorException(responseCode + " " + urlConnection.getResponseMessage() + " getting " + file, responseCode);
            }

            InputStream is = urlConnection.getInputStream();
            BufferedReader r = new BufferedReader(new InputStreamReader(is));
            StringBuilder total = new StringBuilder();
            String line;
            while ((line = r.readLine()) != null) {
                total.append(line);
            }
            return total.toString();
        } catch (MalformedURLException e) {
            throw new HttpErrorException("Could not parse url with host: " + host + " file: " + file, e);
        } catch (IOException e) {
            throw new HttpErrorException("Error reading data", e);
        } finally {
            if(urlConnection != null) {
                urlConnection.disconnect();
            }
        }
    }
}
