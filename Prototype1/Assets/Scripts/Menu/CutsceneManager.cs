using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour
{
    public MenuBehavior mb;
    public string sceneToLoad;
    MainControls mainControls;

    // Start is called before the first frame update
    void OnEnable()
    {
        mainControls = ControlsContainer.instance.mainControls;
        mainControls.Main.Interact.performed += Interact;

    }

    private void OnDisable()
    {
        mainControls.Main.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            Skip();
    }

    // Update is called once per frame
    void Skip()
    {
        mb.LoadScene(sceneToLoad);
    }
}
