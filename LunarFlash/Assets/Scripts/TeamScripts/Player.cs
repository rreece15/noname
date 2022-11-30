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

public class Player : MonoBehaviour
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
    [Header("PayerCanvasSetting")]
    public GameObject HPcanvas;
    public TMP_Text playerHP_text;
    public Slider playerHP_bar;
    public GameObject AmmoCanvas;
    public TMP_Text Ammo_Total;
    public TMP_Text Ammo_Current;

    // Start is called before the first frame update
    void Start()
    {

        this.gameObject.transform.position = new Vector3(380f, 20, 586);
       
        playerController = this.gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
      
      
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
     

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer) // from this API too https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        playerController.Move(playerVelocity * Time.deltaTime);

    }


    public void UpdatePlayerHP(int itemORenemyAttack) // HPpotion - positive number && EnemyAttack - negative number
    {
        playerHP += itemORenemyAttack;
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

    public void UpdateAmmoStatus()
    {
        //Ammo_text.text = ammoQuant.ToString();
       // Ammo_bar.value = ammoQuant;
    }


    private void OnTriggerEnter(Collider other)
    {
       
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


   

}
