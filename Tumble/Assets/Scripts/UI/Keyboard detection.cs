using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboarddetection : MonoBehaviour
{
    [SerializeField] private GameObject azerty, qwerty;

    private void Start()
    {
        azerty.SetActive(Application.systemLanguage == SystemLanguage.French);
        qwerty.SetActive(!(Application.systemLanguage == SystemLanguage.French));

    }
}
