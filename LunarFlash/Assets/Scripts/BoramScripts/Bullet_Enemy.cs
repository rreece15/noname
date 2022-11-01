using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{

    public BulletData enemybullet;
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
        if (other.gameObject.tag == "Destroy")
        {
            enemybullet.CollideWithWall(this.gameObject);
        }
        if(other.gameObject.tag == "PlayerTrigger")
        {
            other.transform.parent.gameObject.GetComponent<Player>().DamageFromEnemyAttack(enemybullet.bulletDamage);
            Destroy(this.gameObject);
        }
 
    }
}
