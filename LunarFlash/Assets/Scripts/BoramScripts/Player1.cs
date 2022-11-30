using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
//using TreeEditor;
//using UnityEngine.Windows;

public class Player1 : MonoBehaviour { } // only for saving
    /*
{
    [Header("PlayerSetting")]
    [SerializeField] private float playerHP;
    [Space]
    [Header("Movement")]
    CharacterController playerController;
    private float jumpHeight = 5.0f;  //UnityAPI : https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float movingSpeed;
    public float camSpeed = 2;
    [Space]
    [Header("BulletSetting")]
    public bool attackable=true;
    public static int maxAmmonQuant=50;
    public int ammoQuant;
    private Vector3 bulletDir;
    private Vector3 bulletPos;
    public GameObject bullet;
    public float ammoChargeTimer;
    public float ammoChargeTime;
    private bool ammoChargeStart=false;
    //public float bulletDamage; moved to BulletData script
    //public float bulletSpeed = 5f;  moved to BulletData script
    [Space]
    [Header("PayerCanvasSetting")]
    public GameObject HPcanvas;
    public TMP_Text playerHP_text;
    public Slider playerHP_bar;
    public GameObject AmmoCanvas;
    public TMP_Text Ammo_Total;
    public TMP_Text Ammo_Current;
    // public Slider Ammo_bar;
    [Space]
    [Header("ExplosionEffect")]
    public GameObject explosionEffect;
    //[Space]
    // [Header("GameEnd")]
    //public GameObject finishCanvas;

    //CamSetting
    Vector3 standardPos = new Vector3(0, 0, 3);
    Vector3 standardPosWorld;
    float newX;
    float newY;
    Vector3 newPos;
    Vector3 mousePosition;
    float mouseX;
    float mouseY;
    Vector3 calculatedMousePos;
    Quaternion toRotation;

    // Start is called before the first frame update
    void Start()
    {
       
        attackable = true;
       // finishCanvas.SetActive(false);
        ammoChargeStart = false;
        playerController = this.gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.lockState = CursorLockMode.Confined;
        // bulletPos = this.gameObject.transform.GetChild(0).gameObject.transform.position;
        maxAmmonQuant = 50;
        ammoQuant = maxAmmonQuant;
        ammoChargeTimer = ammoChargeTime;
        if (playerHP_text == null)
        {
            playerHP_text = HPcanvas.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
            playerHP_text.text = playerHP.ToString();
        }

        if (playerHP_bar == null)
        {
            playerHP_bar = HPcanvas.transform.GetChild(1).gameObject.GetComponent<Slider>();
            playerHP_bar.maxValue = playerHP;
            playerHP_bar.value = playerHP;
        }

        if (Ammo_Total == null)
        {
            //Ammo_text = AmmoCanvas.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
            //Ammo_text.text = ammoQuant.ToString();
        }

      /* if (Ammo_bar == null)
        {
       /     Ammo_bar = AmmoCanvas.transform.GetChild(1).gameObject.GetComponent<Slider>();
            Ammo_bar.maxValue = ammoQuant;
            Ammo_bar.value = ammoQuant;
        }*/

   // }
    /*
     * Screen.width 
     * Screen.height 
     * 
     * 
     */
/*
    // Update is called once per frame
    void Update()
    {
                        //Line 119 - 131 : Cam movement - 1111 - Wow.. I can set FirstPersonCam movement just chaning the cinemachine setting to POV!!!!!!ha..
                        //Vector2 mouseDelta = Mouse.current.delta.ReadValue();
                        //Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 3f);
                        // Debug.Log(mouseDelta);
                        // newX = NormalizeProcessor.Normalize(mouseDelta.x, 0f, Screen.width, Screen.width);
                        // newY = NormalizeProcessor.Normalize(mouseDelta.y, 0f, Screen.height, Screen.height/2);
                        //newX= NormalizeProcessor.Normalize(Input.mousePosition.x, 0f, Screen.width, Screen.width/2);
                        // newY = NormalizeProcessor.Normalize(Input.mousePosition.y, 0f, Screen.height, Screen.height/2);
                        //newPos = new Vector3(newX * Screen.width/2, newY* Screen.height/2, 3f);// -width <x<width && -height < y < height
                        //  newPos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 3f);
                        //Debug.Log(newPos);
                        //mousePosition = Camera.main.ScreenToWorldPoint(newPos);
                        // Debug.Log("MousePosition is " + mousePosition);
                        //  standardPosWorld = Camera.main.ScreenToWorldPoint(standardPos);
                        // mouseY = mousePosition.y - standardPosWorld.y;
                        //mouseX = mousePosition.x - standardPosWorld.x;

                        //calculatedMousePos = new Vector3(transform.position.x + mouseX, transform.position.y+ mouseY, mousePosition.z);
                        //calculatedMousePos = new Vector3(transform.position.x/* + (mouseDelta.x* Screen.width)*///, transform.position.y /*+ (mouseDelta.y * Screen.height)*/, mousePosition.z);
                        //toRotation = Quaternion.LookRotation(calculatedMousePos - transform.position);
                        //bulletDir = mouse; // - transform.position;
                        // transform.forward = transform.position+mouseDelta;//Quaternion.Slerp(transform.rotation, toRotation, camSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
                         // transform.forward = //calculatedMousePos;// - transform.position;
                        //transform.LookAt(calculatedMousePos);// - transform.position);
                        // bulletDir = transform.forward;

      /*  if (attackable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bulletPos = this.gameObject.transform.GetChild(0).gameObject.transform.position;
                Attack();
                // Debug.Log(this.gameObject.transform.GetChild(0).gameObject.transform.position);
            }
        }*/

/*
        groundedPlayer = playerController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
      
        playerController.Move((transform.forward * Input.GetAxis("Vertical")) * Time.deltaTime * movingSpeed);
        playerController.Move((transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * movingSpeed);
     

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer) // from this API too https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        playerController.Move(playerVelocity * Time.deltaTime);

       /* if (ammoChargeStart == true)
        {
            ChargeAmmo();
        }*/
    /*}

    private void Attack()
    {
        if (ammoQuant > 0 && ammoQuant <= maxAmmonQuant)
        {
            var bulletClone = Instantiate(bullet, bulletPos, this.gameObject.transform.rotation);

            bulletClone.GetComponent<Rigidbody>().velocity = (bulletDir - bulletClone.gameObject.transform.position) * bulletClone.GetComponent<Bullet_Player>().data.bulletSpeed;

            ammoQuant--;//bulletClone.GetComponent<Bullet_Player>().data.DecreaseBulletQuantity();
            UpdateAmmoStatus();
            // bulletClone.GetComponent<Bullet_Player>().data.ChargeStart(); // need to change this to Player}

            if (ammoChargeStart == false) { ammoChargeStart = true; }
        }
    }

    public void DamageFromEnemyAttack(float EnemyBulletDamage)
    {
        playerHP -= EnemyBulletDamage;
        UpdatePlayerHP();
    }

    public void UpdatePlayerHP()
    {
        playerHP_text.text = playerHP.ToString();
        playerHP_bar.value = playerHP;
    }

    public void UpdateAmmoStatus()
    {
        //Ammo_text.text = ammoQuant.ToString();
       // Ammo_bar.value = ammoQuant;
    }

    public void ChargeAmmo()
    {
        if (ammoQuant < maxAmmonQuant)
        {
            ammoChargeTimer -= Time.deltaTime;
            if (ammoChargeTimer < 0)
            {
                if ((ammoQuant + 1) <= maxAmmonQuant)
                { ammoQuant += 1;
                    UpdateAmmoStatus();
                }
                    
                ammoChargeTimer = ammoChargeTime; 

            }
        }
        else { ammoChargeStart = false; }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "EnemyCollision")
        {
            other.gameObject.transform.parent.GetComponent<EnemyControl>().enemy1.ExplodeStart(this.gameObject, other.gameObject.transform.parent.gameObject);
            ExplosionEffect();
        }
       if(other.gameObject.tag == "Finish")
        {
          //  attackable = false;
           // AmmoCanvas.SetActive(false);
           // HPcanvas.SetActive(false);
           // finishCanvas.SetActive(true);
        }
       if(other.gameObject.tag == "Item")
        {
            InventoryManager.GetItemInfo(other.gameObject);
            InventoryManager.UpdateInventory();
            Destroy(other.gameObject);
        }
    }

    public void ExplosionEffect()
    {
        explosionEffect.GetComponent<ParticleSystem>().Play();
        explosionEffect.GetComponent<AudioSource>().Play();
    }

   

}
    */