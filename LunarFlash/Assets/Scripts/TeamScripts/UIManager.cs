using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Setting")]
    // Canvas objects and controls
    [SerializeField] private Canvas options;
    [SerializeField] private Canvas optionsOpen;
    [SerializeField] private Button openOptions;
    [SerializeField] private Button closeOptions;
    [SerializeField] private Canvas playerCanvas;
    [SerializeField] private Canvas GameOverWinCanvas;
    [Space]
    [Header("Inventory Setting")]
    [SerializeField] private Canvas InventoryShortcutCanvas;
    //[SerializeField] private Canvas InventoryWholeCanvas;
    GameObject inventoryFullUI;
    [Space]
    [Header("Resolution Setting")]
    // Screen Resolution
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [Header("Volume Setting")]
    // Volume
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeText;
    [Space]
    public GameManager gmforUI;
   // public Light
    float volume;

    bool menuIsOpen;

    static public bool inventoryFullUION=false;
    static public bool inventoryFullUIOFF = false;
    // score
    private int currentScore;
   

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menuIsOpen = false;
        InventoryShortcutCanvas.GetComponent<Canvas>().enabled = false;
        inventoryFullUI = InventoryShortcutCanvas.transform.GetChild(1).gameObject;
        inventoryFullUI.transform.localScale = new Vector3(0, 1, 1);
        //inventoryFulUI.SetActive(false);

        //GameOverMenu
        GameOverWinCanvas.enabled = false;


        optionsOpen.GetComponent<Canvas>().enabled = false;
        
        closeOptions.onClick.AddListener(() => CloseOptions());

        volumeSlider.onValueChanged.AddListener((v) => SetVolume(v));

        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add((resolutions[i].width + " x " + resolutions[i].height));
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            SetResolution(resolutionDropdown.value);
        });

  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            menuIsOpen = true;
            GameObject.FindGameObjectsWithTag("Gun")[0].GetComponent<Gun>().setPaused(true);
            OpenOptions();
        }

        

        if (Input.GetKey(KeyCode.E))
        {
            OpenWholeInventory();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            CloseWholeInventory();
        }

        if (inventoryFullUION == true && inventoryFullUIOFF==false)
        {
            //inventoryFullUI.SetActive(true);
            inventoryFullUI.transform.localScale = new Vector3(1, 1, 1);
            inventoryFullUION = false;
        }
        if (inventoryFullUION == false && inventoryFullUIOFF == true)
        {
            // inventoryFullUI.SetActive(false);
            inventoryFullUI.transform.localScale = new Vector3(0, 1, 1);
            inventoryFullUIOFF = false;
        }

        if (Player.isGameOver)
        {
            OpenGameOverMenu();
        }

        if (Player.isGameClear)
        {
            OpenGameWinMenu();
        }
    }

    void OpenOptions()
    {
        Gun.isGunEnabled = false;
        Time.timeScale = 0;
        options.GetComponent<Canvas>().enabled = false;
        optionsOpen.GetComponent<Canvas>().enabled = true;
        playerCanvas.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void OpenWholeInventory()
    {
        // Time.timeScale = 0;
        InventoryShortcutCanvas.GetComponent<Canvas>().enabled = true;
        InventoryManager.IMInstance.InventoryOpen();
       // optionsOpen.GetComponent<Canvas>().enabled = true;
       // playerCanvas.GetComponent<Canvas>().enabled = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void CloseWholeInventory()
    {
        InventoryShortcutCanvas.GetComponent<Canvas>().enabled = false;
        InventoryManager.IMInstance.InventoryClose();
        //  Cursor.lockState = CursorLockMode.Locked;
    }

    public void CloseOptions()
    {
        Gun.isGunEnabled = true;
        Debug.Log("mouseClick");
        Time.timeScale = 1;
        options.GetComponent<Canvas>().enabled = true;
        optionsOpen.GetComponent<Canvas>().enabled = false;
        playerCanvas.GetComponent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectsWithTag("Gun")[0].GetComponent<Gun>().setPaused(false);
    }

    void SetResolution(int resIndex)
    {
        //Cursor.lockState = CursorLockMode.Confined;
        Gun.isGunEnabled = false;
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        Debug.Log("resolution set to: " + res.width + " x " + res.height);
        // for storing in JSON
        //playerPrefs.res = resolutions[resIndex];
        //playerPrefs.resIndex = resIndex;
        resolutionDropdown.value = resIndex;
    }

    void SetVolume(float newVolume)
    {
        //Cursor.lockState = CursorLockMode.Confined;
        Gun.isGunEnabled = false;
        Debug.Log("mouseClick");
        volumeText.text = "Volume: " + newVolume.ToString("0.00");
        AudioListener.volume = newVolume;
        volume = newVolume;
        // for storing JSON
        //playerPrefs.volume = newVolume;
        volumeSlider.value = newVolume;
    }

    void OpenGameOverMenu()
    {
        gmforUI.ResetLighIntentisy();
        Cursor.lockState = CursorLockMode.Confined;
        Gun.isGunEnabled = false;
        GameOverWinCanvas.enabled = true;
        GameOverWinCanvas.gameObject.transform.GetChild(1).GetComponent<TMP_Text>().enabled = true;
        GameOverWinCanvas.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().enabled = false;
        GameOverWinCanvas.gameObject.transform.GetChild(6).GetComponent<TMP_Text>().text = gmforUI.GetFinalScore().ToString();
        Time.timeScale = 0;
    }

    void OpenGameWinMenu()
    {
        gmforUI.ResetLighIntentisy();
        Cursor.lockState = CursorLockMode.Confined;
        Gun.isGunEnabled = false;
        GameOverWinCanvas.enabled = true;
        GameOverWinCanvas.gameObject.transform.GetChild(1).GetComponent<TMP_Text>().enabled = false;
        GameOverWinCanvas.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().enabled = true;
        GameOverWinCanvas.gameObject.transform.GetChild(6).GetComponent<TMP_Text>().text = gmforUI.GetFinalScore().ToString();
        Time.timeScale = 0;
    }




    //////////////////////////////////Button click functions///////////////////////////////
    ///

    public void ReplayButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
