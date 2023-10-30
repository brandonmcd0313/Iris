using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorMovementController : MonoBehaviour
{
    [SerializeField] float _interactionTime;
    [SerializeField] float _interactionDelay;
    [SerializeField] GameObject _oldLady;
    [SerializeField] GameObject _speechBubblePrefab;
    [SerializeField] Vector3 _speechBubblePosition;
    [SerializeField] GameObject[] _visitorPrefabs;
    [SerializeField] Vector3 _visitorSpawnPoint;
    [SerializeField] Vector3 _visitorDestination;

    GameObject _activeBubble;
    List<GameObject> _activeVisitors = new List<GameObject>();
    string _hasPayedTroyCoinEvent = "p_hasPayedTroyCoin";
    // Start is called before the first frame update
    //must be awake to happen before the scene manager is created
    void Awake()
    {
        _oldLady.GetComponent<Animator>().enabled = false;
            StartCoroutine(VisitorLoop());
    }
    private void Start()
    {
        if (PlayerPrefsManager.HasPlayerPrefBeenActivated(_hasPayedTroyCoinEvent))
        {
            StopAllCoroutines();
            Destroy(_activeBubble);
            Destroy(_oldLady);
            foreach (GameObject visitor in _activeVisitors)
            {
                Destroy(visitor);
            }
        }
        GameObject.Find("Table").GetComponent<Table>().OnPlayerPaysTroyCoin += StopAllVisitors;
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
            _activeVisitors.Add(visitor);
            StartCoroutine(MoveToPositon(visitor));


            //wait for half the interaction time
            yield return new WaitForSeconds(_interactionTime / 2);

            //spawn a speech bubble
            GameObject speechBubble = Instantiate(_speechBubblePrefab, _speechBubblePosition, Quaternion.identity);
            _activeBubble = speechBubble;
            _oldLady.GetComponent<Animator>().enabled = true;

            //wait for the other half of the interaction time
            yield return new WaitForSeconds(_interactionTime / 2);

            //destroy the speech bubble
            Destroy(speechBubble);
            _oldLady.GetComponent<Animator>().enabled = false;
            
            yield return new WaitForSeconds(1f);


            StartCoroutine(MoveBackToStart(visitor));
            //wait for the interaction delay
            yield return new WaitForSeconds(_interactionDelay);
        }

        StartCoroutine(VisitorLoop());
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

    IEnumerator MoveBackToStart(GameObject visitor)
    {
        float timePassed = 0;
        Vector3 startPosition = visitor.transform.position;
        while (timePassed < _interactionTime)
        {
            visitor.transform.position = Vector3.Lerp(_visitorDestination, _visitorSpawnPoint, timePassed / _interactionTime);
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _activeVisitors.Remove(visitor);
        Destroy(visitor);
    }
    IEnumerator MoveBackToStart(GameObject visitor, int time)
    {
        float timePassed = 0;
        Vector3 startPosition = visitor.transform.position;
        while (timePassed < time)
        {
            visitor.transform.position = Vector3.Lerp(_visitorDestination, _visitorSpawnPoint, timePassed / time);
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _activeVisitors.Remove(visitor);
        Destroy(visitor);
    }
    public void StopAllVisitors()
    {
        Destroy(_activeBubble);
        _oldLady.GetComponent<OldLady>().StartFearMoment();
        StopAllCoroutines();
        foreach (GameObject visitor in _activeVisitors)
        {
            StartCoroutine(MoveBackToStart(visitor,1));
        }
    }
}
