using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Homebase : MonoBehaviour
{
    public Text UIPlaceText;
    public Transform collectibles;
    public Collider deliverPoint;
    public Movement player;
    public string playerIsHolding;

    public static Homebase Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


    }

    

    public void PlaceCollectible(string collectibleName)
    {
        foreach (Transform collectible in collectibles)
        {
            if(collectible.name == collectibleName)
            {
                collectible.gameObject.SetActive(true);
                playerIsHolding = "";
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UiScript.Instance.FadeText(true);
            other.gameObject.SendMessage("SetHomeBaseBool", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UiScript.Instance.FadeText(false);
            other.gameObject.SendMessage("SetHomeBaseBool", false);
        }
    }
}
