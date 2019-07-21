using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public GameManager gameManager;
    private Button thisButton;

    private void Start()
    {
        thisButton = GetComponent<Button>();
    }

    public void OnClick()
    {
        thisButton.interactable = false;

        string action = transform.Find("Text").GetComponent<TextMeshProUGUI>().text.ToUpper();
        gameManager.CurrentAction = action;

        foreach(GameObject otherActionButton in GameObject.FindGameObjectsWithTag("ActionButton"))
        {
            if (otherActionButton!=this.gameObject)
                otherActionButton.GetComponent<Button>().interactable = true;
        }
    }
}
