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
        //set reveal point to the top center of the screen
        Vector3 revealPoint = new Vector3(0, 0, 0);
        revealPoint.x = Screen.width / 2;
        revealPoint.y = Screen.height;
        while (true)
        {
            //Move toward set point in the scene
            while (transform.position != revealPoint)
            {
                transform.position = Vector3.MoveTowards(
                transform.position, revealPoint,
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
