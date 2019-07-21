using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Object[] pages;
    public TextMeshProUGUI mainText;

    [Header("Buttons")]
    public GameObject choiceButton;
    public GameObject actionButton;
    public GameObject targetButton;

    [Header("Scenes")]
    public GameObject scenePresentation;
    public GameObject sceneGameplay;
    public GameObject sceneOtherOptions;

    [Header("Panels")]
    public GameObject choicesPanel;
    public GameObject objectsPanel;
    public GameObject interactiveElementsPanel;

    [Header("Parameters")]
    public Page previousPage;
    public Page currentPage;
    [SerializeField]
    public string CurrentAction;
    [SerializeField]
    public string CurrentTarget;


    private List<GameObject> createdObjects;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(this.gameObject);
    }

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

    //setta il testo della currentPage e crea un choiceButton per ogni choice della currentPage
    public void SetPage(Page page)
    {
        foreach (GameObject createdObject in createdObjects)
            Destroy(createdObject);

        mainText.text = currentPage.getFinalText();
        foreach(Choice choice in page.choices)
        {
            //if (requisiti rispettati)
            {
                GameObject newChoiceButton = Instantiate(choiceButton, choicesPanel.transform);
                newChoiceButton.GetComponent<ChoiceButton>().ChoiceCode = choice.code;
                newChoiceButton.GetComponent<ChoiceButton>().GameManager = this;
                createdObjects.Add(newChoiceButton);
                newChoiceButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = choice.text;
            }
        }

        foreach(string result in page.results)
        {
            PlayerPrefs.SetInt(result, 1);
        }
    }

    //cancella tutti gli oggetti nella cache, lascia attiva solo la SceneGameplay e setta la pagina con code 1
    public void SwitchToGameplay()
    {
        foreach (GameObject createdObject in createdObjects)
            Destroy(createdObject);

        createdObjects.Clear();

        if (scenePresentation.activeSelf)
            scenePresentation.SetActive(false);

        if (sceneOtherOptions.activeSelf)
            sceneOtherOptions.SetActive(false);

        sceneGameplay.SetActive(true);
        SetPage(GetPageByCode(1));
    }

    public void SwitchToOtherOptions()
    {
        foreach (GameObject createdObject in createdObjects)
            Destroy(createdObject);

        createdObjects.Clear();

        CurrentAction = null;
        CurrentTarget = null;

        sceneGameplay.SetActive(false);
        sceneOtherOptions.SetActive(true);

        foreach(string interactiveObject in currentPage.interactiveObjects)
        {
            GameObject newObjectButton = Instantiate(targetButton, interactiveElementsPanel.transform);
            newObjectButton.GetComponent<TargetButton>().gameManager = this;
            createdObjects.Add(newObjectButton);
            newObjectButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = interactiveObject;
        }
    }

    public void OnOKButtonPressed()
    {
        SwitchToGameplay();

        Debug.Log("OK Button pressed!");
        if (CurrentAction != null && CurrentTarget != null)
        {
            OnChoiceTaken(CurrentAction, CurrentTarget);
        }

        else
            Debug.LogError("Error: missing Action or Target!");
        
    }

    public void OnChoiceTaken(string choiceCode, string target)
    {
        Debug.Log("SCELTA PRESA: [" + currentPage.name + "] ---> (" + choiceCode + ", " + target + ")");
        int newPageCode = PageFinder.GetNextPageCode(currentPage.code, choiceCode, target);
        Debug.Log("Codice prossima pagina: " + newPageCode);
        previousPage = currentPage;
        currentPage = GetPageByCode(newPageCode);
        Debug.Log("Nome prossima pagina: " + currentPage.name);
        SetPage(currentPage);
    }

    public Page GetPageByCode(int pageCode)
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
