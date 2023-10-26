using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //loading levels
using UnityEngine.UI; //UI text and images
using TMPro; //UI Text Mesh Pro

public class MissingPosterFinalScene : MonoBehaviour, IInteractable
{
    [SerializeField] Color _defaultColor;
    [SerializeField] Color _highlightColor;
    //[SerializeField] GameObject _posterPrefab;
    SpriteRenderer _sr;
    public GameObject _loadingScreen;

    void Start()
    {
        //set the default color
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = _defaultColor;
        _loadingScreen.SetActive(false);
    }

    public void OnPlayerApproach()
    {
        _sr.color = _highlightColor;
    }

    public void OnPlayerInteract()
    {
        //Instantiate(_posterPrefab, transform.position, Quaternion.identity);
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadEndScene());
    }

    public void ResetToDefaults()
    {
        _sr.color = _defaultColor;
    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadEndScene()
    {
        for (int i = 0; i <= 255; i += 5)
        {
            //fade the overlay out
            _loadingScreen.GetComponent<Image>().color =
                new Color(0, 0, 0, i / 255.0f);
            yield return new WaitForSeconds(0.02f);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("End3_MissingPosterRevealScene");
        //SceneManager.LoadScene("Park 1_Starting Scene");
    }
}