using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryIcon : MonoBehaviour
{
    GameObject[] spots;
    string _bagEvent = "p_hasPlayerBagBeenGrabed";
    [SerializeField] Sprite _troyCoinIcon;
    [SerializeField] Sprite _candyIcon;
    [SerializeField] Sprite _defaultBagIcon;
    [SerializeField] Sprite _openBagIcon;
    static InventoryIcon instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //destroy this gameobject
            Destroy(this.gameObject);
        }
        
    }
    // Start is called before the first frame update
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    
        print("scene loaded");
        //set the spots array to the children of this gameobject
        spots = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spots[i] = transform.GetChild(i).gameObject;
        }
      
        //bag not activated\

        
        if (!PlayerPrefsManager.HasPlayerPrefBeenActivated(_bagEvent))
        {
            Invoke("FirstTimeHookUp", 0.1f);
            HideBag();
        }
        
        else
        {
            SpawnInventoryIcon();
        }
        
    }

    void OpenBag()
    {
        //change sprite to open bag
        GetComponent<SpriteRenderer>().sprite = _openBagIcon;
        //enable all of the spots
        foreach (GameObject spot in spots)
        {
            spot.SetActive(true);
        }
        //get the inventory
        string[] inventory = PlayerPrefsManager.GetItemsInInventory();
        //set the sprite of each spot to the correct sprite
        for (int i = 0; i < inventory.Length; i++)
        {
            switch (inventory[i])
            {
                case "troycoin":
                    spots[i].GetComponent<SpriteRenderer>().sprite = _troyCoinIcon;
                    break;
                case "candy":
                    spots[i].GetComponent<SpriteRenderer>().sprite = _candyIcon;
                    break;
                default:
                    //disable the spot
                    spots[i].SetActive(false);
                    break;
            }
        }
    }

    void CloseBag()
    {
        //set to default bag icon
        GetComponent<SpriteRenderer>().sprite = _defaultBagIcon;
        //disable all of the spots and set their sprites to null
        foreach (GameObject spot in spots)
        {
            spot.GetComponent<SpriteRenderer>().sprite = null;
            spot.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        OpenBag();
    }

    private void OnMouseExit()
    {
        CloseBag();
    }

    void FirstTimeHookUp()
    {
        //find an object with the playerbag script
        if (GameObject.FindGameObjectsWithTag("Bag").Length ==  0)
        {
            return;
        }
        GameObject bag = GameObject.FindGameObjectsWithTag("Bag")[0];
        PlayerBag playerBagScript = bag.GetComponent<PlayerBag>();
        playerBagScript.OnPlayerGrabBag += SpawnInventoryIcon;
    }

    void SpawnInventoryIcon()
    {
        print("spawn inventory icon");
        //set the sprite to the default bag icon
        GetComponent<SpriteRenderer>().sprite = _defaultBagIcon;
        foreach (GameObject spot in spots)
        {
            spot.GetComponent<SpriteRenderer>().sprite = null;
            spot.SetActive(false);
        }
    }

    void HideBag()
    {
        //set gameobject sprite to null
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        foreach (GameObject spot in spots)
        {
            spot.GetComponent<SpriteRenderer>().sprite = null;
            spot.SetActive(false);
        }
    }
}
