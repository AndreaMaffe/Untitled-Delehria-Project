using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TargetButton : MonoBehaviour
{
    public GameManager gameManager { get; set; }
    private Button thisButton;

    private void Start()
    {
        thisButton = GetComponent<Button>();
    }

    public void OnClick()
    {
        thisButton.interactable = false;
        string element = transform.Find("Text").GetComponent<TextMeshProUGUI>().text.ToUpper();
        gameManager.CurrentTarget = element;

        foreach (GameObject otherTargetButton in GameObject.FindGameObjectsWithTag("TargetButton"))
        {
            if (otherTargetButton != this.gameObject)
                otherTargetButton.GetComponent<Button>().interactable = true;
        }
    }
}
