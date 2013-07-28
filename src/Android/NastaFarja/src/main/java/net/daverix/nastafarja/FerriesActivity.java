package net.daverix.nastafarja;

import android.os.Bundle;
import android.view.Menu;
import android.support.v7.app.ActionBarActivity;

public class FerriesActivity extends ActionBarActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ferries);


    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.ferries, menu);
        return true;
    }
    
}
