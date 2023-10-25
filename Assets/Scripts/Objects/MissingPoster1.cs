using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingPoster1 : MonoBehaviour
{
    //this is on the prefab
    public bool IsDoingAnimation;
    bool _hasBeenDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        if (IsDoingAnimation)
        {
            //start at scale 0 0 0 
            transform.localScale = Vector3.zero;
            //start the coroutine
            StartCoroutine(GrowToScale(10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if any button is hit destroy this object
        if (Input.anyKey && !_hasBeenDestroyed && !(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)))
        {
            _hasBeenDestroyed = true;
            Invoke("DestroyObject", 0.1f);
        }
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    IEnumerator GrowToScale(int size)
    {
        //over 2 seconds grow to size
        for (float i = 0; i < 1.25f; i += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(size, size, size), i / 1.25f);
            yield return null;
        }
    }
}
