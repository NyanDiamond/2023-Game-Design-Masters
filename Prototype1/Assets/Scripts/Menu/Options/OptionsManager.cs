using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] mouseNavText, controllerNavText;

    private InputChecker inputChecker;
    private bool isController = false;

    // Start is called before the first frame update
    void Start()
    {
        inputChecker = FindObjectOfType<InputChecker>();

        if (inputChecker.IsController())
            SetController();
        else
            SetMouse();
    }

    // Update is called once per frame
    void Update()
    {
        if (isController != inputChecker.IsController())
        {
            isController = inputChecker.IsController();

            if (isController)
                SetController();

            else
                SetMouse();
        }
    }

    private void SetMouse()
    {
        for(int i = 0; i < mouseNavText.Length; i++)
        {
            mouseNavText[i].gameObject.SetActive(true);
            controllerNavText[i].gameObject.SetActive(false);
        }
    }

    private void SetController()
    {
        for (int i = 0; i < mouseNavText.Length; i++)
        {
            mouseNavText[i].gameObject.SetActive(false);
            controllerNavText[i].gameObject.SetActive(true);
        }
    }
}
