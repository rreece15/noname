using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Player : MonoBehaviour
{

    public BulletData data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (data.ChargeStart())
        {
            data.BulletCharge();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Destroy")
        {
            data.CollideWithWall(this.gameObject);
        }
        if(other.gameObject.tag == "DamageDetector")
        {
            data.CollideWithEnemy(other.gameObject.transform.parent.gameObject);
            other.gameObject.transform.parent.gameObject.GetComponent<EnemyControl>().UpdateHP();
            Destroy(this.gameObject);
        }
    }
}
