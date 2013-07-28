package net.daverix.nastafarja.test;
import android.test.AndroidTestCase;

import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.DepartureTime;
import net.daverix.nastafarja.model.Ferry;
import net.daverix.nastafarja.util.GsonParser;
import net.daverix.nastafarja.model.Info;
import net.daverix.nastafarja.util.ParseException;
import net.daverix.nastafarja.util.Parser;

/**
 * Created by daverix on 7/27/13.
 */
public class GsonParserTest extends AndroidTestCase {
    private Parser mParser;

    @Override
    protected void setUp() throws Exception {
        super.setUp();

        mParser = new GsonParser();
    }

    public void testParseInfo() throws ParseException {
        String json = "{\"Info\":[{\"Name\":\"Kornhallsleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Kornhallsleden/\",\"DepartsFrom\":[\"Kornhall\"]},{\"Name\":\"Hönöleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Honoleden/\",\"DepartsFrom\":[\"Hönö\",\"Lilla Varholmen\"]},{\"Name\":\"Sund Jarenleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/SundJarenleden/\",\"DepartsFrom\":[\"Sund\",\"Jaren\"]},{\"Name\":\"Hamburgsundsleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Hamburgsundleden/\",\"DepartsFrom\":[\"Hamburgö\"]},{\"Name\":\"Bohus Malmönleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/BohusMalmonleden/\",\"DepartsFrom\":[\"Malmön\"]},{\"Name\":\"Björköleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Bjorkoleden/\",\"DepartsFrom\":[\"Björkö\",\"Lilla Varholmen\"]},{\"Name\":\"Gullmarsleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Gullmarsleden/\",\"DepartsFrom\":[\"Finnsbo\",\"Skår\"]},{\"Name\":\"Lyrleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Angoleden1/\",\"DepartsFrom\":[\"Lyresten\"]},{\"Name\":\"Ängöleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Angoleden1/\",\"DepartsFrom\":[\"Ängö\"]},{\"Name\":\"Malöleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Angoleden1/\",\"DepartsFrom\":[\"Malö\"]}]}";
        Info obj = mParser.parse(json, Info.class);

        assertNotNull(obj);
        assertNotNull(obj.getFerries());
        assertTrue(obj.getFerries().size() > 0);
        Ferry ferry = obj.getFerries().get(0);
        assertNotNull(ferry);
        assertEquals("Kornhallsleden", ferry.getName());
        assertEquals("Västra Götaland", ferry.getRegion());
        assertEquals("http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Kornhallsleden/", ferry.getUrl());
        assertNotNull(ferry.getDepartsFrom());
        assertTrue(ferry.getDepartsFrom().size() > 0);
        assertEquals("Kornhall", ferry.getDepartsFrom().get(0));
    }

    public void testParseDepartures() throws ParseException {
        String json = "{\"Name\":\"Kornhallsleden\",\"Title\":\"Kornhall-Gunnesby-Kornhall*\",\"DepartsFrom\":\"Kornhall\",\"ArrivesAt\":\"Gunnesby\",\"NextDepartures\":[{\"TimeOfDay\":\"14:15\",\"Attribute\":null},{\"TimeOfDay\":\"14:35\",\"Attribute\":null},{\"TimeOfDay\":\"14:55\",\"Attribute\":null},{\"TimeOfDay\":\"15:15\",\"Attribute\":null},{\"TimeOfDay\":\"15:35\",\"Attribute\":null},{\"TimeOfDay\":\"15:55\",\"Attribute\":null},{\"TimeOfDay\":\"16:15\",\"Attribute\":null},{\"TimeOfDay\":\"16:35\",\"Attribute\":null},{\"TimeOfDay\":\"16:55\",\"Attribute\":null},{\"TimeOfDay\":\"17:15\",\"Attribute\":null}]}";

        Departure departure = mParser.parse(json, Departure.class);

        assertNotNull(departure);
        assertEquals("Kornhallsleden", departure.getName());
        assertEquals("Kornhall-Gunnesby-Kornhall*", departure.getTitle());
        assertEquals("Kornhall", departure.getDepartsFrom());
        assertEquals("Gunnesby", departure.getArrivesAt());
        assertNotNull(departure.getNextDepartures());
        assertTrue(departure.getNextDepartures().size() > 0);
        DepartureTime time = departure.getNextDepartures().get(0);
        assertNotNull(time);
        assertEquals("14:15", time.getTimeOfDay());
    }

    @Override
    protected void tearDown() throws Exception {
        super.tearDown();

        mParser = null;
    }
}
