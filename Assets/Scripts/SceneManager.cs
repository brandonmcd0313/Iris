using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [TooltipAttribute("Set to -1 to disable")]
    [SerializeField] int _nextSceneIndex;

    [TooltipAttribute("Set to -1 to disable")]
    [SerializeField] int _previousSceneIndex;

    [Space(5)]
    GameObject _player;
    SceneLocker _sceneLocker;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<SceneLocker>() != null)
        {
            _sceneLocker = GetComponent<SceneLocker>();
        }
        _player = GameObject.FindGameObjectWithTag("Player"); 
        _player.GetComponent<PlayerController>().OnPlayerExitScreenSpaceRight += LoadNextScene;
        _player.GetComponent<PlayerController>().OnPlayerExitScreenSpaceLeft += LoadPreviousScene;

    }

    void LoadNextScene()
    {
        if(_sceneLocker != null)
        {
            if(_sceneLocker.IsLockedToTheRight && _sceneLocker.IsLocked)
            {
                return;
            }
        }
        //load the next scene
        if (_nextSceneIndex != -1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_nextSceneIndex);
        }
    }

    void LoadPreviousScene()
    {
        if (_sceneLocker != null)
        {
            if (!_sceneLocker.IsLockedToTheRight && _sceneLocker.IsLocked)
            {
                return;
            }
        }
        //load the previous scene
        if (_previousSceneIndex != -1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_previousSceneIndex);
        }

    }
}
