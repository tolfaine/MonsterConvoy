using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hard code garbage.

public class InvasionManager : MonoBehaviour
{

    int loop = 0;
    int turn = 0;
    GameObject[] pawns;
    bool[] activeInvasions = new bool[25];
    int[] turnsPerInvasion = { 2, 4, 6, 13, 16, 18, 21, 25 };

    bool afterStart = false;

    private void Start()
    {
        pawns = GameObject.FindGameObjectsWithTag("MapNode");



        //Sort this array based on id.
        for (int i = 0; i < pawns.Length; ++i)
        {
            GameObject temp;
            temp = pawns[pawns[i].GetComponent<Id>().id];
            pawns[pawns[i].GetComponent<Id>().id] = pawns[i];
            pawns[i] = temp;
        }

        for (int i = 0; i < activeInvasions.Length; ++i)
        {
            activeInvasions[i] = false;
        }

        afterStart = true;
    }

    private void OnEnable()
    {
        if (afterStart)
            newTurn();
    }

    void newTurn()
    {
        pawns[35].GetComponent<PlaceType>().SetInvaded(); //Portal is always invaded. 

        switch (loop)
        {
            #region loop 1
            case (0):
                switch (turn)
                {
                    case (0):
                        for (int i = 14; i < 29; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (1):
                        for (int i = 0; i < 35; ++i)
                        {
                            if(!pawns[i].GetComponent<PlaceType>().invasionStatus)
                                pawns[i].GetComponent<PlaceType>().SetInvaded();  
                        }
                        break;
                }
                break;
            #endregion
            #region loop 2
            case (1):
                switch (turn)
                {
                    case (0):
                        for (int i = 11; i <= 21; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (1):
                        for (int i = 22; i <= 29; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (2):
                        for (int i = 3; i <= 10; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (3):
                        for (int i = 0; i < 35; ++i)
                        {
                            if (!pawns[i].GetComponent<PlaceType>().invasionStatus)
                                pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                }
                break;
            #endregion
            #region loop 3
            case (2):
                switch (turn)
                {
                    case (0):
                        for (int i = 16; i <= 20; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (1):
                        for (int i = 21; i <= 27; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (2):
                        for (int i = 8; i <= 15; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (3):
                        for (int i = 28; i <= 32; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (4):
                        for (int i = 3; i <= 7; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (5):
                        for (int i = 0; i < 35; ++i)
                        {
                            if (!pawns[i].GetComponent<PlaceType>().invasionStatus)
                                pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                }
                break;
            #endregion
            #region loop 4
            case (3):
                switch (turn)
                {
                    case (0):
                        for (int i = 18; i <= 20; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (1):
                        for (int i = 21; i <= 22; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (2):
                        for (int i = 16; i <= 17; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (3):
                        for (int i = 23; i <= 26; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (4):
                        for (int i = 14; i <= 15; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (5):
                        for (int i = 27; i <= 29; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (6):
                        for (int i = 11; i <= 13; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (7):
                        pawns[10].GetComponent<PlaceType>().SetInvaded();
                        pawns[30].GetComponent<PlaceType>().SetInvaded();
                        pawns[31].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (8):
                        for (int i = 7; i <= 9; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        pawns[5].GetComponent<PlaceType>().SetInvaded();

                        break;
                    case (9):
                        for (int i = 32; i <= 34; ++i)
                        {
                            pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                    case (10):
                        pawns[3].GetComponent<PlaceType>().SetInvaded();
                        pawns[4].GetComponent<PlaceType>().SetInvaded();
                        pawns[6].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (11):
                        pawns[1].GetComponent<PlaceType>().SetInvaded();
                        pawns[2].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (12):
                        for (int i = 0; i < 35; ++i)
                        {
                            if (!pawns[i].GetComponent<PlaceType>().invasionStatus)
                                pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                }
                break;
            #endregion
            #region loop 5
            case (4):
                switch (turn)
                {
                    case (0):
                        pawns[18].GetComponent<PlaceType>().SetInvaded();
                        pawns[19].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (1):
                        pawns[20].GetComponent<PlaceType>().SetInvaded();
                        pawns[21].GetComponent<PlaceType>().SetInvaded();
                        pawns[22].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (2):
                        pawns[16].GetComponent<PlaceType>().SetInvaded();
                        pawns[17].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (3):
                        pawns[23].GetComponent<PlaceType>().SetInvaded();
                        pawns[24].GetComponent<PlaceType>().SetInvaded();
                        pawns[25].GetComponent<PlaceType>().SetInvaded();
                        pawns[26].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (4):
                        pawns[13].GetComponent<PlaceType>().SetInvaded();
                        pawns[14].GetComponent<PlaceType>().SetInvaded();
                        pawns[15].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (5):
                        pawns[27].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (6):
                        pawns[11].GetComponent<PlaceType>().SetInvaded();
                        pawns[12].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (7):
                        pawns[10].GetComponent<PlaceType>().SetInvaded();
                        pawns[28].GetComponent<PlaceType>().SetInvaded();
                        pawns[29].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (8):
                        pawns[8].GetComponent<PlaceType>().SetInvaded();
                        pawns[9].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (9):
                        pawns[30].GetComponent<PlaceType>().SetInvaded();
                        pawns[31].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (10):
                        pawns[5].GetComponent<PlaceType>().SetInvaded();
                        pawns[7].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (11):
                        pawns[32].GetComponent<PlaceType>().SetInvaded();
                        pawns[34].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (12):
                        pawns[4].GetComponent<PlaceType>().SetInvaded();
                        pawns[6].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (13):
                        pawns[2].GetComponent<PlaceType>().SetInvaded();
                        pawns[33].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (14):
                        pawns[1].GetComponent<PlaceType>().SetInvaded();
                        pawns[3].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (15):
                        for (int i = 0; i < 35; ++i)
                        {
                            if (!pawns[i].GetComponent<PlaceType>().invasionStatus)
                                pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                }
                break;
            #endregion
            #region loop 6
            case (5):
                switch (turn)
                {
                    case (0):
                        pawns[18].GetComponent<PlaceType>().SetInvaded();
                        pawns[19].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (1):
                        pawns[20].GetComponent<PlaceType>().SetInvaded();
                        pawns[21].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (2):
                        pawns[16].GetComponent<PlaceType>().SetInvaded();
                        pawns[17].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (3):
                        pawns[22].GetComponent<PlaceType>().SetInvaded();
                        pawns[23].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (4):
                        pawns[14].GetComponent<PlaceType>().SetInvaded();
                        pawns[15].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (5):
                        pawns[24].GetComponent<PlaceType>().SetInvaded();
                        pawns[25].GetComponent<PlaceType>().SetInvaded();
                        pawns[26].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (6):
                        pawns[12].GetComponent<PlaceType>().SetInvaded();
                        pawns[13].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (7):
                        pawns[27].GetComponent<PlaceType>().SetInvaded();
                        pawns[28].GetComponent<PlaceType>().SetInvaded();
                        pawns[29].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (8):
                        pawns[8].GetComponent<PlaceType>().SetInvaded();
                        pawns[9].GetComponent<PlaceType>().SetInvaded();
                        pawns[10].GetComponent<PlaceType>().SetInvaded();
                        pawns[11].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (9):
                        pawns[30].GetComponent<PlaceType>().SetInvaded();
                        pawns[31].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (10):
                        pawns[5].GetComponent<PlaceType>().SetInvaded();
                        pawns[7].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (11):
                        pawns[32].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (12):
                        pawns[4].GetComponent<PlaceType>().SetInvaded();
                        pawns[6].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (13):
                        pawns[33].GetComponent<PlaceType>().SetInvaded();
                        pawns[34].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (14):
                        pawns[3].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (15):
                        pawns[2].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (16):
                        pawns[1].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (17):
                        for (int i = 0; i < 35; ++i)
                        {
                            if (!pawns[i].GetComponent<PlaceType>().invasionStatus)
                                pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                }
                break;
            #endregion
            #region loop 7
            case (6):
                switch (turn)
                {
                    case (0):
                        pawns[19].GetComponent<PlaceType>().SetInvaded();
                        pawns[19].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (1):
                        pawns[20].GetComponent<PlaceType>().SetInvaded();
                        pawns[21].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (2):
                        pawns[17].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (3):
                        pawns[22].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (4):
                        pawns[16].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (5):
                        pawns[23].GetComponent<PlaceType>().SetInvaded();
                        pawns[24].GetComponent<PlaceType>().SetInvaded();
                        pawns[25].GetComponent<PlaceType>().SetInvaded();
                        pawns[26].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (6):
                        pawns[14].GetComponent<PlaceType>().SetInvaded();
                        pawns[15].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (7):
                        pawns[27].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (8):
                        pawns[13].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (9):
                        pawns[28].GetComponent<PlaceType>().SetInvaded();
                        pawns[29].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (10):
                        pawns[11].GetComponent<PlaceType>().SetInvaded();
                        pawns[12].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (11):
                        pawns[10].GetComponent<PlaceType>().SetInvaded();
                        pawns[30].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (12):
                        pawns[8].GetComponent<PlaceType>().SetInvaded();
                        pawns[9].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (13):
                        pawns[31].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (14):
                        pawns[5].GetComponent<PlaceType>().SetInvaded();
                        pawns[7].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (15):
                        pawns[32].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (16):
                        pawns[4].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (17):
                        pawns[33].GetComponent<PlaceType>().SetInvaded();
                        pawns[34].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (18):
                        pawns[3].GetComponent<PlaceType>().SetInvaded();
                        pawns[6].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (19):
                        pawns[2].GetComponent<PlaceType>().SetInvaded();
                        break;
                    case (20):
                        for (int i = 0; i < 35; ++i)
                        {
                            if (!pawns[i].GetComponent<PlaceType>().invasionStatus)
                                pawns[i].GetComponent<PlaceType>().SetInvaded();
                        }
                        break;
                }
                break;
            #endregion
            #region loop 8
            case (7):
                #region Hardcoded priorities
                pawns[0].GetComponent<Id>().priority = 24;
                pawns[1].GetComponent<Id>().priority = 22;
                pawns[2].GetComponent<Id>().priority = 23;
                pawns[3].GetComponent<Id>().priority = 20;
                pawns[4].GetComponent<Id>().priority = 18;
                pawns[5].GetComponent<Id>().priority = 16;
                pawns[6].GetComponent<Id>().priority = 18;
                pawns[7].GetComponent<Id>().priority = 16;
                pawns[8].GetComponent<Id>().priority = 12;
                pawns[9].GetComponent<Id>().priority = 14;
                pawns[10].GetComponent<Id>().priority = 15;
                pawns[11].GetComponent<Id>().priority = 10;
                pawns[12].GetComponent<Id>().priority = 10;
                pawns[13].GetComponent<Id>().priority = 8;
                pawns[14].GetComponent<Id>().priority = 6;
                pawns[15].GetComponent<Id>().priority = 8;
                pawns[16].GetComponent<Id>().priority = 5;
                pawns[17].GetComponent<Id>().priority = 4;
                pawns[18].GetComponent<Id>().priority = 2;
                pawns[19].GetComponent<Id>().priority = 0;
                pawns[20].GetComponent<Id>().priority = 1;
                pawns[21].GetComponent<Id>().priority = 3;
                pawns[22].GetComponent<Id>().priority = 5;
                pawns[23].GetComponent<Id>().priority = 7;
                pawns[24].GetComponent<Id>().priority = 9;
                pawns[25].GetComponent<Id>().priority = 9;
                pawns[26].GetComponent<Id>().priority = 7;
                pawns[27].GetComponent<Id>().priority = 11;
                pawns[28].GetComponent<Id>().priority = 13;
                pawns[29].GetComponent<Id>().priority = 13;
                pawns[30].GetComponent<Id>().priority = 17;
                pawns[31].GetComponent<Id>().priority = 19;
                pawns[32].GetComponent<Id>().priority = 21;
                pawns[33].GetComponent<Id>().priority = 23;
                pawns[34].GetComponent<Id>().priority = 21;
                pawns[35].GetComponent<Id>().priority = 0;
                #endregion
                for (int i = 0; i < 35; ++i)
                {
                    if ( pawns[i].GetComponent<Id>().priority <= turn && !pawns[i].GetComponent<PlaceType>().invasionStatus)
                    {
                        pawns[i].GetComponent<PlaceType>().SetInvaded();
                    }
                }
                break;
            #endregion
            #region loop 8 repeated
            default:
                #region Hardcoded priorities
                pawns[0].GetComponent<Id>().priority = 24;
                pawns[1].GetComponent<Id>().priority = 22;
                pawns[2].GetComponent<Id>().priority = 23;
                pawns[3].GetComponent<Id>().priority = 20;
                pawns[4].GetComponent<Id>().priority = 18;
                pawns[5].GetComponent<Id>().priority = 16;
                pawns[6].GetComponent<Id>().priority = 18;
                pawns[7].GetComponent<Id>().priority = 16;
                pawns[8].GetComponent<Id>().priority = 12;
                pawns[9].GetComponent<Id>().priority = 14;
                pawns[10].GetComponent<Id>().priority = 15;
                pawns[11].GetComponent<Id>().priority = 10;
                pawns[12].GetComponent<Id>().priority = 10;
                pawns[13].GetComponent<Id>().priority = 8;
                pawns[14].GetComponent<Id>().priority = 6;
                pawns[15].GetComponent<Id>().priority = 8;
                pawns[16].GetComponent<Id>().priority = 5;
                pawns[17].GetComponent<Id>().priority = 4;
                pawns[18].GetComponent<Id>().priority = 2;
                pawns[19].GetComponent<Id>().priority = 0;
                pawns[20].GetComponent<Id>().priority = 1;
                pawns[21].GetComponent<Id>().priority = 3;
                pawns[22].GetComponent<Id>().priority = 5;
                pawns[23].GetComponent<Id>().priority = 7;
                pawns[24].GetComponent<Id>().priority = 9;
                pawns[25].GetComponent<Id>().priority = 9;
                pawns[26].GetComponent<Id>().priority = 7;
                pawns[27].GetComponent<Id>().priority = 11;
                pawns[28].GetComponent<Id>().priority = 13;
                pawns[29].GetComponent<Id>().priority = 13;
                pawns[30].GetComponent<Id>().priority = 17;
                pawns[31].GetComponent<Id>().priority = 19;
                pawns[32].GetComponent<Id>().priority = 21;
                pawns[33].GetComponent<Id>().priority = 23;
                pawns[34].GetComponent<Id>().priority = 21;
                pawns[35].GetComponent<Id>().priority = 0;
                #endregion
                for (int i = 0; i < 35; ++i)
                {
                    if (pawns[i].GetComponent<Id>().priority <= turn && !pawns[i].GetComponent<PlaceType>().invasionStatus)
                    {
                        pawns[i].GetComponent<PlaceType>().SetInvaded();
                    }
                }
                break;
                #endregion
        }
        if (pawns[0].GetComponent<PlaceType>().invasionStatus)
        {
            newLoop();
        }
        turn++;
    }

    void newLoop()
    {
        loop++;
        for (int i = 0; i < 35; ++i)
        {
            pawns[i].GetComponent<PlaceType>().invasionStatus = false;
        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("MapFlag").Length; ++i)
        {
            Destroy(GameObject.FindGameObjectsWithTag("MapFlag")[i]);
        }

        turn = -1;

        PawnManager.Instance().RegenerateMap();

        pawns = GameObject.FindGameObjectsWithTag("MapNode");

        //Sort this array based on id.
        for (int i = 0; i < pawns.Length; ++i)
        {
            GameObject temp;
            temp = pawns[pawns[i].GetComponent<Id>().id];
            pawns[pawns[i].GetComponent<Id>().id] = pawns[i];
            pawns[i] = temp;
        }
    }
}
