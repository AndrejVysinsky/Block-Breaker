using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StarText : MonoBehaviour
{
    void Start()
    {
        var textField = GetComponent<TextMeshProUGUI>();

        textField.text = ApplicationDataManager.Instance.GetNumberOfOwnedStars().ToString();
    }
}
