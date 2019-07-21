using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionButton : MonoBehaviour
{
    public GameManager gameManager { get; set; }

    public void OnClick()
    {
        string action = transform.Find("Text").GetComponent<TextMeshProUGUI>().text.ToUpper();
        gameManager.CurrentAction = action;
    }
}
