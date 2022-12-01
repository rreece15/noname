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
using Unity.VisualScripting;
//using TreeEditor;
//using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [Header("PlayerSetting")]
    [SerializeField] private float playerHP;
    [Space]
    [Header("Movement")]
    CharacterController playerController;
    private float jumpHeight = 8.0f;  //UnityAPI : https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    private float gravityValue = -12;//-9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float movingSpeed;
    public float camSpeed = 2;
    public float heightchange = 0.02f;
    public GameObject dashUI;
    [Space]
    [Header("PayerCanvasSetting")]
    public GameObject HPcanvas;
    public TMP_Text playerHP_text;
    public Slider playerHP_bar;
    public GameObject AmmoCanvas;
    public TMP_Text Ammo_Total;
    public TMP_Text Ammo_Current;
    [Space]
    [Header("SFXs")]
    public AudioClip inventoryFull;
    public AudioClip dashSound;
    AudioSource playerAudioSource;

    private Vector3 playerCurrentPos;
    float maxHeight = 2.25f;
    bool heightIncrease;
    float minHeight = 1.75f;
    bool heightDecrease;
    float defaultMovingSpeed;
    bool isDashOn = false;

    public static bool isGameOver=false;
    public static bool isGameClear = false;
    // Start is called before the first frame update
    void Start()
    {
        defaultMovingSpeed = movingSpeed;
        isGameOver = false;
        isGameClear = false;
        this.gameObject.transform.position = new Vector3(380f, 10, 586);
       
        playerController = this.gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        playerAudioSource = this.gameObject.GetComponent<AudioSource>();

        movingSpeed = 8;
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

        heightIncrease = true;
        heightDecrease = false;

    }
  
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
                       
        groundedPlayer = playerController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
      
        playerController.Move((transform.forward * Input.GetAxis("Vertical")) * Time.deltaTime * movingSpeed);
        playerController.Move((transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * movingSpeed);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isDashOn == false)
            {
                StartCoroutine(DashMovement());
            }
        }

       /* if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDashOn = false;
        }*/

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer) // from this API too https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += (gravityValue*1.5f) * Time.deltaTime;
        playerController.Move(playerVelocity * Time.deltaTime);

        if(playerHP == 0)
        {
            //GAmeOver
            playerHP = -1;
            isGameOver = true;
            Time.timeScale = 0;
        }

    }

    IEnumerator DashMovement()
    {
        playerAudioSource.clip = dashSound;
        playerAudioSource.Play();
        dashUI.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1, 1, 1, 0.2f);
        isDashOn = true;
        movingSpeed *= 5;

        yield return new WaitForSeconds(2f);

        movingSpeed = defaultMovingSpeed;

        yield return new WaitForSeconds(8f);
        isDashOn = false;
        dashUI.transform.GetChild(0).GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
    }


    public void DecreasePlayerHP(int enemyAttack) // HPpotion - positive number && EnemyAttack - negative number
    {
        playerHP -= enemyAttack;

        if(playerHP <= 0) { playerHP = 0; }
        playerHP_text.text = playerHP.ToString();
        playerHP_bar.value = playerHP;
    }

    public void IncreasePlayerHP(int HPpotion) // HPpotion - positive number && EnemyAttack - negative number
    {
       
        playerHP += HPpotion;

        if(playerHP >= 100) { playerHP = 100; }

        playerHP_text.text = playerHP.ToString();
        playerHP_bar.value = playerHP;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Item")
        {
            if (InventoryManager.isInventoryLocked == false)
            {
                InventoryManager.GetItemInfo(other.gameObject);
                InventoryManager.UpdateInventory();
                Destroy(other.gameObject);
            }
            else
            {
                playerAudioSource.clip = inventoryFull;
                playerAudioSource.Play();
            }
        }
    }

    public float GetPlayerHP()
    {
        return playerHP;
    }

    /*public float DamagePlayerHP(int damage)
    {
        playerHP -= damage;
        playerHP_text.text = playerHP.ToString();
        playerHP_bar.value = playerHP;
        return playerHP;
    }*/
   

}
