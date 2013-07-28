package net.daverix.nastafarja.view;

import net.daverix.nastafarja.model.Ferry;

import java.util.List;

/**
 * Created by daverix on 7/28/13.
 */
public interface FerryListRowFactory {
    public List<FerryListRow> createRows(List<Ferry> ferries);
}
