package net.daverix.nastafarja.test;

import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.DepartureTime;
import net.daverix.nastafarja.model.Ferry;
import net.daverix.nastafarja.model.Info;
import net.daverix.nastafarja.util.GsonParser;
import net.daverix.nastafarja.util.ParseException;
import net.daverix.nastafarja.util.Parser;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;

import static org.hamcrest.CoreMatchers.not;
import static org.hamcrest.CoreMatchers.nullValue;
import static org.hamcrest.core.Is.is;
import static org.hamcrest.core.IsEqual.equalTo;
import static org.junit.Assert.assertThat;

/**
 * Created by daverix on 7/27/13.
 */
@RunWith(RobolectricGradleTestRunner.class)
public class GsonParserTest {
    private Parser mParser;

    @Before
    public void setUp() throws Exception {

        mParser = new GsonParser();
    }

    @Test
    public void testParseInfo() throws ParseException {
        String json = "{\"Info\":[{\"Name\":\"Kornhallsleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Kornhallsleden/\",\"DepartsFrom\":[\"Kornhall\"]},{\"Name\":\"Hönöleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Honoleden/\",\"DepartsFrom\":[\"Hönö\",\"Lilla Varholmen\"]},{\"Name\":\"Sund Jarenleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/SundJarenleden/\",\"DepartsFrom\":[\"Sund\",\"Jaren\"]},{\"Name\":\"Hamburgsundsleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Hamburgsundleden/\",\"DepartsFrom\":[\"Hamburgö\"]},{\"Name\":\"Bohus Malmönleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/BohusMalmonleden/\",\"DepartsFrom\":[\"Malmön\"]},{\"Name\":\"Björköleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Bjorkoleden/\",\"DepartsFrom\":[\"Björkö\",\"Lilla Varholmen\"]},{\"Name\":\"Gullmarsleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Gullmarsleden/\",\"DepartsFrom\":[\"Finnsbo\",\"Skår\"]},{\"Name\":\"Lyrleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Angoleden1/\",\"DepartsFrom\":[\"Lyresten\"]},{\"Name\":\"Ängöleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Angoleden1/\",\"DepartsFrom\":[\"Ängö\"]},{\"Name\":\"Malöleden\",\"Region\":\"Västra Götaland\",\"Url\":\"http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Angoleden1/\",\"DepartsFrom\":[\"Malö\"]}]}";
        Info obj = mParser.parse(json, Info.class);

        assertThat(obj, is(not(nullValue())));
        assertThat(obj.getFerries(), is(not(nullValue())));
        assertThat(obj.getFerries().size() > 0, is(true));

        Ferry ferry = obj.getFerries().get(0);
        assertThat(ferry, is(not(nullValue())));

        assertThat(ferry.getName(), is(equalTo("Kornhallsleden")));
        assertThat(ferry.getRegion(), is(equalTo("Västra Götaland")));
        assertThat(ferry.getUrl(), is(equalTo("http://www.trafikverket.se/Farja/Farjeleder/Farjeleder-i-ditt-lan/Farjeleder-i-Vastra-Gotaland1/Kornhallsleden/")));
        assertThat(ferry.getDepartsFrom(), is(not(nullValue())));
        assertThat(ferry.getDepartsFrom().size() > 0, is(true));
        assertThat(ferry.getDepartsFrom().get(0), is(equalTo("Kornhall")));
    }

    @Test
    public void testParseDepartures() throws ParseException {
        String json = "{\"Name\":\"Kornhallsleden\",\"Title\":\"Kornhall-Gunnesby-Kornhall*\",\"DepartsFrom\":\"Kornhall\",\"ArrivesAt\":\"Gunnesby\",\"NextDepartures\":[{\"TimeOfDay\":\"14:15\",\"Attribute\":null},{\"TimeOfDay\":\"14:35\",\"Attribute\":null},{\"TimeOfDay\":\"14:55\",\"Attribute\":null},{\"TimeOfDay\":\"15:15\",\"Attribute\":null},{\"TimeOfDay\":\"15:35\",\"Attribute\":null},{\"TimeOfDay\":\"15:55\",\"Attribute\":null},{\"TimeOfDay\":\"16:15\",\"Attribute\":null},{\"TimeOfDay\":\"16:35\",\"Attribute\":null},{\"TimeOfDay\":\"16:55\",\"Attribute\":null},{\"TimeOfDay\":\"17:15\",\"Attribute\":null}]}";

        Departure departure = mParser.parse(json, Departure.class);

        assertThat(departure, is(not(nullValue())));
        assertThat(departure.getName(), is(equalTo("Kornhallsleden")));
        assertThat(departure.getTitle(), is(equalTo("Kornhall-Gunnesby-Kornhall*")));
        assertThat(departure.getDepartsFrom(), is(equalTo("Kornhall")));
        assertThat(departure.getArrivesAt(), is(equalTo("Gunnesby")));
        assertThat(departure.getNextDepartures(), is(not(nullValue())));
        assertThat(departure.getNextDepartures().size() > 0, is(true));

        DepartureTime time = departure.getNextDepartures().get(0);
        assertThat(time, is(not(nullValue())));
        assertThat(time.getTimeOfDay(), is(equalTo("14:15")));
    }

    @After
    public void tearDown() throws Exception {
        mParser = null;
    }
}
