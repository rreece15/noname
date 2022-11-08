using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyShell : MonoBehaviour
{
    public ParticleSystem shells;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shells.Emit(1);
        }
    }
}
