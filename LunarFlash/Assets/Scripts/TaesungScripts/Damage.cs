using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public bool damageReady = true;
    AudioSource attack;
    // Start is called before the first frame update
    void Start()
    {
        if(attack == null)
        {
            attack = this.gameObject.GetComponent<AudioSource>();
        }

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
        //GameManager.Instance.playerHealth--;
        // GameManager.Instance.playerHealth--;
        // GameManager.Instance.playerHealth--;
        // GameManager.Instance.playerHealth--;
        // GameManager.Instance.playerHealth--;
        attack.Play();
        GameManager.Instance.playerScript.DecreasePlayerHP(5);
        yield return new WaitForSeconds(1);
        damageReady = true;
    }
}
