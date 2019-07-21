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
                    case "ATTENDI": return 5;
                    default: return 1;
                }





        }






        Debug.LogError("Errore: impossibile calcolare il codice della prossima pagina");
        return 0;
    }
}
