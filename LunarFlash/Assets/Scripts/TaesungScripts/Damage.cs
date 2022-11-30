using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public bool damageReady = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && damageReady == true)
        {
            StartCoroutine(Damaging());
            damageReady = false;
        }
    }
    IEnumerator Damaging()
    {
        GameManager.Instance.playerHealth--;
        GameManager.Instance.playerHealth--;
        GameManager.Instance.playerHealth--;
        GameManager.Instance.playerHealth--;
        GameManager.Instance.playerHealth--;
        yield return new WaitForSeconds(1);
        damageReady = true;
    }
}
