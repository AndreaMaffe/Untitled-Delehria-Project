using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField]
    public string ChoiceCode { get; set; }
    public GameManager GameManager { get; set; }

    public void OnClick()
    {
        GameManager.OnChoiceTaken(ChoiceCode, null);
    }
}
