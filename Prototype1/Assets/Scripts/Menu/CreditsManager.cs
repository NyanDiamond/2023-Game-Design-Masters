using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private GameObject skipPanel;
    [SerializeField] private Image skipProgressBar;
    [SerializeField] private Animator skipPanelAnim;

    [SerializeField] private TextMeshProUGUI skipText;

    MainControls mainControls;

    private bool canSkip = true;
    private bool skipping = false;
    private bool triggeredAway = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        mainControls = ControlsContainer.instance.mainControls;
        mainControls.Main.Interact.performed += ProgressBar;
        mainControls.Main.Interact.canceled += CancelProgress;

        string skipSprite = mainControls.Main.Interact.bindings[0].ToDisplayString().ToUpper().TranslateToSprite();
        skipText.text = "Hold<size=28>" + skipSprite + "</size>to Skip";
    }

    private void Update()
    {
        if (Input.anyKeyDown && skipPanel.activeInHierarchy == false)
        {
            skipPanel.SetActive(true);

            Invoke("Away", 2.0f);
        }

        if (skipping)
        {
            triggeredAway = false;

            if (!skipPanel.activeInHierarchy)
                skipPanel.SetActive(true);

            float currentFill = skipProgressBar.fillAmount;
            skipProgressBar.fillAmount = Mathf.MoveTowards(currentFill, 1.1f, 1 * Time.deltaTime);

            if (currentFill >= 1)
                Skip();
        }

        else if (!skipping)
        {
            skipProgressBar.fillAmount = 0;

            if (!triggeredAway && skipPanel.activeInHierarchy)
            {
                triggeredAway = true;
                Invoke("Away", 2.0f);
            }
        }
    }

    private void ProgressBar(InputAction.CallbackContext context)
    {
        if (canSkip)
        {
            skipping = true;
            CancelInvoke("Away");
        }
    }

    private void CancelProgress(InputAction.CallbackContext context)
    {
        if (canSkip)
            skipping = false;
    }

    // Update is called once per frame
    void Skip()
    {
        if (canSkip)
        {
            canSkip = false;
            Invoke("LoadNextScene", 0.1f);
        }
    }

    private void LoadNextScene()
    {
        SceneLoader.Instance.LoadSceneQuick("MainMenu_New");
    }

    private void Skip(UnityEngine.Video.VideoPlayer vid)
    {
        Skip();
    }

    private void Away()
    {
        //animation here
        skipPanel.SetActive(false);
    }
}
