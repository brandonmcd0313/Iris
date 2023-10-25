using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class OldLady : MonoBehaviour
{
    [SerializeField] Vector2 _endPosition;

    [SerializeField] float shiverDuration = 2f;
    [SerializeField] float shiverDistance = 0.5f;
    [SerializeField] float shiverCount = 4;
    void Start()
    {
        StartFearMoment();
    }
   public void StartFearMoment()
    {
        StartCoroutine(FearMoment());
    }

  
    IEnumerator FearMoment()
    {

        Vector3 startPosition = transform.position;
        float timePassed = 0f;

        while (timePassed < shiverDuration)
        {

            transform.position = new Vector3(startPosition.x + (shiverDistance * Mathf.Sin(timePassed * Mathf.PI * shiverCount)), startPosition.y, startPosition.z);
            yield return null; // Wait for one frame

            timePassed += Time.deltaTime;
        }

        //move to the end position
        timePassed = 0;
        startPosition = transform.position;
        while (timePassed < 1.5f)
        {
            transform.position = Vector3.Lerp(startPosition, _endPosition, timePassed / 1.5f);
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);

    }



}
