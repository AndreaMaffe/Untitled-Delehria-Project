using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveElementButton : MonoBehaviour
{
    public GameManager gameManager { get; set; }

    public void OnClick()
    {
        string element = transform.Find("Text").GetComponent<TextMeshProUGUI>().text.ToUpper();
        gameManager.CurrentTarget = element;
    }
}
