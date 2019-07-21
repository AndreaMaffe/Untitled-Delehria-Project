using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFinder
{
    public static int GetNextPageCode(int currentPageCode, string choiceCode, string target)
    {
        switch(currentPageCode)
        {
            case 1: switch(choiceCode)
                {
                    case "ATTENDI": return 4;
                    case "GUARDA": if (target == "KUZ") return 2; break;
                    case "PARLA": if (target == "KUZ") return 3; break;
                }
                break;

        }


        switch(choiceCode)
        {
            case "INDIETRO": return GameManager.instance.previousPage.code;
            case "GUARDA": return -1;
            case "PARLA": return -1;
        }



        return -1;
    }
}
