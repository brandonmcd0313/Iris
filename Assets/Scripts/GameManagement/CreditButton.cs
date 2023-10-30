using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //loading levels
using UnityEngine.UI; //UI text and images
using TMPro; //UI Text Mesh Pro

public class CreditButton : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip _contSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCredits()
    {
        //load credit scene
        _audioSource.PlayOneShot(_contSound);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void ToMainMenuButton()
    {
        _audioSource.PlayOneShot(_contSound);
        PlayerPrefs.DeleteAll();

        UnityEngine.SceneManagement.SceneManager.LoadScene("OnGameStart");
    }
}
