using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneManager : MonoBehaviour
{

    [SerializeField] GameObject _birdPrefab;
    [SerializeField] GameObject _bagPrefab;
    [SerializeField] Vector2[] _birdSpawnPoints;
    [SerializeField] Vector2 _bagSpawnPoint;

    PlayerBag _playerBagScript;
    
    string _bagEvent = "p_hasPlayerBagBeenGrabed";
    string _birdEvent = "p_haveBirdsBeenScared";
    private void Awake()
    {
        //create player prefs for birds scared and bag collected
        PlayerPrefsManager.CreatePlayerPref(_bagEvent);
        PlayerPrefsManager.CreatePlayerPref(_birdEvent);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefsManager.HasPlayerPrefBeenActivated(_birdEvent))
        {
            //spawn birds
            for (int i = 0; i < _birdSpawnPoints.Length; i++)
            {
                Instantiate(_birdPrefab, _birdSpawnPoints[i], Quaternion.identity);
            }
        }

        if (PlayerPrefsManager.HasPlayerPrefBeenActivated(_bagEvent))
        {
            //spawn bag
           GameObject bag = Instantiate(_bagPrefab, _bagSpawnPoint, Quaternion.identity);
            _playerBagScript = bag.GetComponent<PlayerBag>();
            _playerBagScript.OnPlayerGrabBag += OnBagPickup;
        }
        
        
    }

    // Update is called once per frame
    void OnBagPickup()
    {
        PlayerPrefsManager.ActivatePlayerPref(_bagEvent);

        PlayerPrefsManager.ActivatePlayerPref(_birdEvent);
    }
    
}
