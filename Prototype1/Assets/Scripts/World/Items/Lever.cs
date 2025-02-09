using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IPullable
{
    [SerializeField] Material[] materials;
    [SerializeField] GameObject target;
    IToggleable it;
    private MeshRenderer mr;
    private bool toggle;
    Light lt;

    // Start is called before the first frame update
    void Start()
    {
        mr = gameObject.GetComponentInParent<MeshRenderer>();
        it = target.GetComponentInChildren<IToggleable>();
        toggle = it.GetToggle();
        lt = GetComponentInParent<Light>();
        UpdateMaterial();
    }


    void UpdateMaterial()
    {
        if (toggle)
        {
            mr.material = materials[1];
            lt.intensity = 1;
        }
        else
        {
            mr.material = materials[0];
            lt.intensity = 0;
        }
    }

    public void Lassoed()
    {
        return;
    }

    public void Pulled(IsoAttackManager player = null)
    {
        toggle = !toggle;
        it.Toggle();
        UpdateMaterial();
    }

    public void Break()
    {

    }
}
