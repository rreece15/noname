using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletDataScriptableObject", order = 2)]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public float bulletDamage;
    public float bulletChargingTime;
    //public float damage;
    public int maxBulletQuantity;
    public float bulletSpeed;
    [Space]
    [Header("Runtime Values")]
    public int currentBulletQuantity;
    public float bulletChargingTimer;
    public bool chargingStart;


    

   //common Functions
   public void CollideWithWall(GameObject bullet)
    {
        GameObject.Destroy(bullet);
    }

    //PlayerBulletData
    public void SetUpBullet()
    {
        bulletSpeed = 5;
        bulletDamage = 10;
        chargingStart = false;
        currentBulletQuantity = maxBulletQuantity;
        bulletChargingTimer = bulletChargingTime;
    }
    public void CollideWithEnemy(GameObject enemy) 
    { 
        enemy.GetComponent<EnemyControl>().enemy1.enemyHP-=bulletDamage;
        
    }

    public void BulletCharge()
    {
        if (currentBulletQuantity < maxBulletQuantity)
        {
            bulletChargingTimer-=Time.deltaTime;
            if (bulletChargingTimer < 0)
            {
                currentBulletQuantity += 5;
                bulletChargingTimer = bulletChargingTime;
            }
        }
    }

    public void DecreaseBulletQuantity()
    {
        currentBulletQuantity--;
    }

    public bool ChargeStart()
    {
        chargingStart = true;
        return chargingStart;
    }

    //EnemyBulletData
    public void CollideWithPlayer(GameObject player, BulletData enemyBulletData)
    {
        player.GetComponent<Player>().DamageFromEnemyAttack(enemyBulletData.bulletDamage);
    }

    public void SetUpEnemyBullet()
    {
        bulletDamage = 15;
    }

}
