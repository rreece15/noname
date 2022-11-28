using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Setting")]
    // Canvas objects and controls
    [SerializeField] private Canvas options;
    [SerializeField] private Canvas optionsOpen;
    [SerializeField] private Button openOptions;
    [SerializeField] private Button closeOptions;
    [SerializeField] private Canvas playerCanvas;
    [Space]
    [Header("Inventory Setting")]
    [SerializeField] private Canvas InventoryShortcutCanvas;
    [SerializeField] private Canvas InventoryWholeCanvas;
    [Space]
    [Header("Resolution Setting")]
    // Screen Resolution
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [Header("Volume Setting")]
    // Volume
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeText;
    float volume;

    // score
    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {

        InventoryWholeCanvas.GetComponent<Canvas>().enabled = false;
       
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
    }

    void OpenOptions()
    {
        Time.timeScale = 0;
        options.GetComponent<Canvas>().enabled = false;
        optionsOpen.GetComponent<Canvas>().enabled = true;
        playerCanvas.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void OpenWholeInventory()
    {
        // Time.timeScale = 0;
        InventoryWholeCanvas.GetComponent<Canvas>().enabled = true;
       // optionsOpen.GetComponent<Canvas>().enabled = true;
       // playerCanvas.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void CloseWholeInventory()
    {
        InventoryWholeCanvas.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CloseOptions()
    {
        Debug.Log(Input.mousePosition.x + " " + Input.mousePosition.y);
        Time.timeScale = 1;
        options.GetComponent<Canvas>().enabled = true;
        optionsOpen.GetComponent<Canvas>().enabled = false;
        playerCanvas.GetComponent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void SetResolution(int resIndex)
    {
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
        volumeText.text = "Volume: " + newVolume.ToString("0.00");
        AudioListener.volume = newVolume;
        volume = newVolume;
        // for storing JSON
        //playerPrefs.volume = newVolume;
        volumeSlider.value = newVolume;
    }
}
