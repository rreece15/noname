using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSphere : MonoBehaviour
{
    public bool canSeeThePlayer;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            canSeeThePlayer = true;
        }
    }
}

