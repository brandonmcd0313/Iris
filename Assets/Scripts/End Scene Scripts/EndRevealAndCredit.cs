using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //loading levels
using UnityEngine.UI; //UI text and images
using TMPro; //UI Text Mesh Pro
using UnityEditor;

public class EndRevealAndCredit : MonoBehaviour
{
   //public GameObject _missingPoster;
   // public GameObject _overlay;
    public int ScreenHeightPositionInPercent = 50;

    public float _revealSpeed;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RevealPoster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RevealPoster()
    {
        Vector3 endPosition = new Vector3(0,0, 0);
        endPosition.x = Screen.width / 2;
        endPosition.y = Screen.height * (ScreenHeightPositionInPercent / 100f);
        while (true)
        {
            //Move toward set point in the scene
            while (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(
                transform.position, endPosition,
                    _revealSpeed * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            //Wait when get to the point
            yield return new WaitForSeconds(1.5f);

        }
    }

    /*
    IEnumerator RevealPoster()
    {
        _overlay.SetActive(true);
        
        for (int i = 225; i >= 0; i -= 5)
        {
                //fade the overlay in
                _overlay.GetComponent<Image>().color =
                    new Color(0, 0, 0, i / 255.0f);
                yield return new WaitForSeconds(1.5f);
        }
            _overlay.SetActive(false);
        

    }
    */

}
