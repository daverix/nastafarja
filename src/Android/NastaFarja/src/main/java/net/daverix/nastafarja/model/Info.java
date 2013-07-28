package net.daverix.nastafarja.model;

import com.google.gson.annotations.SerializedName;

import net.daverix.nastafarja.model.Ferry;

import java.util.List;

/**
 * Created by daverix on 7/27/13.
 */
public class Info {
    @SerializedName(value = "Info")
    private List<Ferry> mFerries;

    public Info() {

    }

    public List<Ferry> getFerries() {
        return mFerries;
    }
}
