using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorMovementController : MonoBehaviour
{
    [SerializeField] float _interactionTime;
    [SerializeField] float _interactionDelay;
    [SerializeField] GameObject _oldLady;
    [SerializeField] GameObject _speechBubblePrefab;
    [SerializeField] GameObject[] _visitorPrefabs;
    [SerializeField] Vector3 _visitorSpawnPoint;
    [SerializeField] Vector3 _visitorDestination;
    private string _sceneLoadEvent = "p_hasLoadedTrunkOrTreatEntrance";
    // Start is called before the first frame update
    //must be awake to happen before the scene manager is created
    void Awake()
    {
        if(!PlayerPrefsManager.HasPlayerPrefBeenActivated(_sceneLoadEvent))
        {
            
        }
        else
        {
            Destroy(this);
        }
    }

   IEnumerator VisitorLoop()
    {
        //shuffle the visitor prefab array
        for (int i = 0; i < _visitorPrefabs.Length; i++)
        {
            GameObject temp = _visitorPrefabs[i];
            int randomIndex = Random.Range(i, _visitorPrefabs.Length);
            _visitorPrefabs[i] = _visitorPrefabs[randomIndex];
            _visitorPrefabs[randomIndex] = temp;
        }

        //spawn a vistor at its spawn point and move it to its destination
        for (int i = 0; i < _visitorPrefabs.Length; i++)
        {
            GameObject visitor = Instantiate(_visitorPrefabs[i], _visitorSpawnPoint, Quaternion.identity);
            StartCoroutine(MoveToPositon(visitor));
            //wait for half the interaction time
            yield return new WaitForSeconds(_interactionTime / 2);
            //spawn a speech bubble
            GameObject speechBubble = Instantiate(_speechBubblePrefab, visitor.transform.position, Quaternion.identity);
            //wait for the other half of the interaction time
            yield return new WaitForSeconds(_interactionTime / 2);
            //destroy the speech bubble
            Destroy(speechBubble);

            //wait for the interaction delay
            yield return new WaitForSeconds(_interactionDelay);
        }
    }

    IEnumerator MoveToPositon(GameObject visitor)
    {
        float timePassed = 0;
        Vector3 startPosition = visitor.transform.position;
        while (timePassed < _interactionTime)
        {
            visitor.transform.position = Vector3.Lerp(startPosition, _visitorDestination, timePassed / _interactionTime);
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
