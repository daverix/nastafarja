package net.daverix.nastafarja.view;
import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public class NextFerryDeparturesAdapter implements DeparturesAdapter {
    private final List<String> mTimes;

    public NextFerryDeparturesAdapter(List<String> times) {
        mTimes = times;
    }

    @Override
    public String getTime(int position) {
        return mTimes.get(position);
    }
}
