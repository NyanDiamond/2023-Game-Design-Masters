using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BarrierController : MonoBehaviour
{
    [SerializeField] barrierShader[] barriersList;

    [SerializeField] private EventReference appearSound;

    // Start is called before the first frame update
    void Awake()
    {
        barriersList = GetComponentsInChildren<barrierShader>(true);

        ToggleBarrier(true);
    }

    public void ToggleBarrier(bool toggle)
    {
        foreach (barrierShader shader in barriersList)
        {
            shader.ToggleBarrier(toggle);
        }

        if(!toggle)
        {
            Invoke("DisableCollider", 0.4f);
            Invoke("DisableBarrier", 2.5f);
        }
        AudioManager.instance.PlayOneShot(appearSound, this.transform.position);
    }

    private void DisableCollider()
    {
        GameObject collider = GetComponentInChildren<BoxCollider>().gameObject;
        collider.SetActive(false);
    }

    private void DisableBarrier()
    {
        gameObject.SetActive(false);
    }
}
