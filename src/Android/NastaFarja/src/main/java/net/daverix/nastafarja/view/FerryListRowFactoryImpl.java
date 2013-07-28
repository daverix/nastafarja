package net.daverix.nastafarja.view;

import net.daverix.nastafarja.model.Departure;
import net.daverix.nastafarja.model.DepartureTime;
import net.daverix.nastafarja.model.Ferry;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public class FerryListRowFactoryImpl implements FerryListRowFactory {
    public FerryListRowFactoryImpl() {

    }

    private List<String> getTimes(List<DepartureTime> depTimes) {
        List<String> times = new ArrayList<String>();

        if(depTimes != null) {
            for(DepartureTime time : depTimes) {
                times.add(time.getTimeOfDay());
            }
        }
        return times;
    }

    private void addDepartureRows(List<FerryListRow> rows, Ferry ferry) {
        List<Departure> departures = ferry.getDepartures();

        if(departures != null) {
            for(Departure departure : departures) {
                if(departure != null) {
                    List<DepartureTime> depTimes = departure.getNextDepartures();
                    if(depTimes != null) {
                        rows.add(new DepartureRow(departure.getTitle(), getTimes(depTimes)));
                    }
                }
            }
        }
    }

    @Override
    public List<FerryListRow> createRows(List<Ferry> ferries) {
        List<FerryListRow> rows = new ArrayList<FerryListRow>();

        for(Ferry ferry : ferries) {
            rows.add(new FerryHeaderRow(ferry.getName()));

            addDepartureRows(rows, ferry);
        }

        return rows;
    }
}
