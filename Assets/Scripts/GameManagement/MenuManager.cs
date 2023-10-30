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
    public Button _loadFromSaveButton;
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
        PlayerPrefs.DeleteAll();
        _audioSource.PlayOneShot(_startSound);
        //enable loading screen disable button
        _loadingScreen.SetActive(true);
        _startGameButton.interactable = false;
        if(_loadFromSaveButton != null)
        {

            _loadFromSaveButton.interactable = false;
        }

        //Start scene transition 
        StartCoroutine(LoadFirstScene());
    }

    public void LoadFromSave()
    {
        string _shouldLoadPreviousActive = "p_shouldLoadPreviousActiveScene";
        PlayerPrefsManager.ActivatePlayerPref(_shouldLoadPreviousActive);
        _audioSource.PlayOneShot(_startSound);
        //enable loading screen disable button
        _loadingScreen.SetActive(true);
        _startGameButton.interactable = false;
        if (_loadFromSaveButton != null)
        {

            _loadFromSaveButton.interactable = false;
        }

        //Start scene transition 
        StartCoroutine(LoadFirstScene());
    }

    IEnumerator LoadFirstScene()
    {
        //spawn full black screen for half second
      
        _loadingScreen.GetComponent<Image>().color =
                new Color(0, 0, 0, 1);
            yield return new WaitForSeconds(0.5f);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Park1_StartingScene");
        //SceneManager.LoadScene("Park 1_Starting Scene");
    }
}
