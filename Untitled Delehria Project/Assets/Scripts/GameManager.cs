using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Object[] pages;
    public Page currentPage;
    public TextMeshProUGUI mainText;
    public GameObject button;

    [Header("Scenes")]
    public GameObject scenePresentation;
    public GameObject sceneGameplay;
    public GameObject sceneOtherOptions;

    [Header("Panels")]
    public GameObject choicesPanel;
    public GameObject objectsPanel;
    public GameObject interactiveElementsPanel;


    private List<GameObject> createdObjects;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        createdObjects = new List<GameObject>();

        pages = Resources.LoadAll("Pages", typeof(Page));

        SwitchToGameplay();
    }

    public void SetPlayerBackground(int value)
    {
        PlayerPrefs.SetInt("BACKGROUND", value);
    }

    public void SetPlayerGoal(int value)
    {
        PlayerPrefs.SetInt("GOAL", value);
    }

    public void IncreaseTrait(string traitName)
    {
        int actualValue = PlayerPrefs.GetInt(traitName, 0);

        actualValue++;
        if (actualValue > 2)
            actualValue = 2;

        PlayerPrefs.SetInt(traitName, actualValue);
    }

    public void SetPage(Page page)
    {
        mainText.text = currentPage.getFinalText();
        foreach(Choice choice in page.choices)
        {
            //if (requisiti rispettati)
            GameObject newChoiceButton = Instantiate(button, choicesPanel.transform);
            createdObjects.Add(newChoiceButton);
            newChoiceButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = choice.text;
        }

        foreach(string result in page.results)
        {
            PlayerPrefs.SetInt(result, 1);
        }
    }

    public void SwitchToGameplay()
    {
        foreach (GameObject createdObject in createdObjects)
            Destroy(createdObject);

        createdObjects.Clear();

        try
        {
            if (scenePresentation.activeSelf)
                scenePresentation.SetActive(false);
        }
        catch (System.NullReferenceException e) { }

        try
        {
            if (sceneOtherOptions.activeSelf)
                sceneOtherOptions.SetActive(false);
        }
        catch (System.NullReferenceException e) { }

        sceneGameplay.SetActive(true);
        SetPage(GetPage(1));
    }

    public void SwitchToOtherOptions()
    {
        foreach (GameObject createdObject in createdObjects)
            Destroy(createdObject);

        createdObjects.Clear();

        sceneGameplay.SetActive(false);
        sceneOtherOptions.SetActive(true);

        foreach(string interactiveObject in currentPage.interactiveObjects)
        {
            GameObject newObjectButton = Instantiate(button, interactiveElementsPanel.transform);
            createdObjects.Add(newObjectButton);
            newObjectButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = interactiveObject;
        }
    }

    public void OnChoiceTaken(int currentPageCode, string choiceCode)
    {
        int newPageCode = PageFinder.GetNextPageCode(currentPageCode, choiceCode, null);
        currentPage = GetPage(newPageCode);
        SetPage(currentPage);
    }

    public void OnChoiceTaken(int currentPageCode, string choiceCode, string target)
    {
        int newPageCode = PageFinder.GetNextPageCode(currentPageCode, choiceCode, target);
        currentPage = GetPage(newPageCode);
        SetPage(currentPage);
    }

    public Page GetPage(int pageCode)
    {
        foreach(var page in pages)
        {
            if (((Page)page).code == pageCode)
                return (Page)page;
        }

        Debug.LogError("No pages found with code" + pageCode);
        return null;
    }


}
