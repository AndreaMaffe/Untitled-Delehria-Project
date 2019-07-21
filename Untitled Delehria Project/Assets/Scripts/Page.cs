using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page", menuName = "Page", order = 1)]
public class Page : ScriptableObject
{
    public int code;
    public string text;
    public TextVariant[] textVariants;
    public Choice[] choices;
    public string[] interactiveObjects;
    public string[] results;

    public string getFinalText()
    {
        string finalText = text;

        for(int i=0; i<textVariants.Length; i++)
        {
            if (finalText.Contains("*" + (i + 1)))
                finalText = finalText.Replace("*" + (i+1), getCorrectTextVariant(textVariants[i]));
        }

        for (int i = 0; i < textVariants.Length; i++)
        {
            if (finalText.Contains("&" + (i + 1)))
                finalText = finalText.Replace("&" + (i + 1), getCorrectTextValue(textVariants[i]));
        }

        return finalText;
    }

    public string getCorrectTextVariant(TextVariant textVariant)
    {
        return textVariant.outcomes[PlayerPrefs.GetInt(textVariant.referenceTrait)];
    }

    public string getCorrectTextValue(TextVariant textVariant)
    {
        return PlayerPrefs.GetString(textVariant.referenceTrait);
    }
}

[System.Serializable]
public struct TextVariant
{
    public string referenceTrait;
    public string[] outcomes;
}

[System.Serializable]
public struct Choice
{
    public string requirement;
    public string text;
}





