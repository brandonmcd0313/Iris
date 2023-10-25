using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //loading levels
using UnityEngine.UI; //UI text and images
using TMPro; //UI Text Mesh Pro

public class EndScene : MonoBehaviour
{
    public GameObject _loadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        _loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D _otherObject)
    {
        if (_otherObject.tag == "Player")
        {
            _loadingScreen.SetActive(true);
            StartCoroutine(LoadEndScene());
        }
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
