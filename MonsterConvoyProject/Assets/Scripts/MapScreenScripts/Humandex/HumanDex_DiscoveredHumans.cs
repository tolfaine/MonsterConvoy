using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parses the human files. Looks up the correct image necessary and displays the accurate tip. 
//We also need to know if we've discovered this human otherwise we place a blank box with a question mark

public class HumanDex_DiscoveredHumans : MonoBehaviour {

    private int pageNumber = 0;
    private int totalPages = 5;

    private void Start()
    {
        /*Create images until we get to the end of the screen
         * Augment by line height. Repeat.
         * Once we get to the end of the screen we pause there. 
         * More images on the secnod page. 
         * Probably want to keep number of humans per page consistent. So that we don't get any freaky image size redisplay wackiness.
         * Sooooo.. .Once we have all these empty squares ??? 
         * Create a file
         */
         
        int counter = 0;
        string line;

        // Read the file and display it line by line.
        System.IO.StreamReader file =
           new System.IO.StreamReader("c:\\test.txt");
        while ((line = file.ReadLine()) != null)
        {
            //Line.1-3;
            //Whether or not we have discovered it. If we haven't break immediately. Perhaps read that info first
            //Name of the thing we're looking up so we can find the image, relevant tips and flavour.
            counter++;
        }

        file.Close();

    }
    

    public void TurnPageRight()
    {
        if (pageNumber != totalPages)
            pageNumber++;
    }

    public void TurnPageLeft()
    {
        if (pageNumber != 0)
            pageNumber--;
    }

}
