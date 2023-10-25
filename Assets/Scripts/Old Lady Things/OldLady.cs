using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldLady : MonoBehaviour
{
    [SerializeField] Vector2 _endPosition;

    [SerializeField] float shiverDuration = 2f;
    [SerializeField] float shiverDistance = 0.5f;
    [SerializeField] float shiverCount = 4;
  
   public void StartFearMoment()
    {
        StartCoroutine(FearMoment());
    }

  
    IEnumerator FearMoment()
    {
        //stop anim
        GetComponent<Animator>().enabled = false;
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
