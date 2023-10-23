using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //loading levels
using UnityEngine.UI; //UI text and images
using TMPro; //UI Text Mesh Pro


public class MenuManager : MonoBehaviour
{
    public GameObject _loadingScreen;

    public Button _startGameButton; //starts the button
    AudioSource _audioSource;
    public AudioClip _startSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        _audioSource.PlayOneShot(_startSound);
        //enable loading screen disable button
        _loadingScreen.SetActive(true);
        _startGameButton.interactable = false;

        //Start scene transition 
        StartCoroutine(LoadFirstScene());
    }


    IEnumerator LoadFirstScene()
    {
        for (int i = 0; i <= 255; i += 5)
        {
            //fade the overlay out
            _loadingScreen.GetComponent<Image>().color =
                new Color(0, 0, 0, i / 255.0f);
            yield return new WaitForSeconds(0.02f);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Park1_StartingScene");
        //SceneManager.LoadScene("Park 1_Starting Scene");
    }
}
