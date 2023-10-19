using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkOrTreatEntranceManager : MonoBehaviour
{
   private string _sceneLoadEvent = "p_hasLoadedTrunkOrTreatEntrance";
    private string _troyCoinAbility = "p_canSpawnTroyCoin";
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefsManager.ActivatePlayerPref(_sceneLoadEvent);
        PlayerPrefsManager.ActivatePlayerPref(_troyCoinAbility);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
