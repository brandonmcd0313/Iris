using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllOldObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //destroy all objects tagged dont destroy on load
        GameObject[] objects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
