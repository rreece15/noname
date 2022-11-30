using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    AudioSource menu_Audio;

    private void Start()
    {
        menu_Audio = this.GetComponent<AudioSource>();
    }
    public void Play()
    {
        menu_Audio.Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        menu_Audio.Play();
        Application.Quit();
    }
}
