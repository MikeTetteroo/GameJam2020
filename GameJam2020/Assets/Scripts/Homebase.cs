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
            //StartCoroutine(FadeTextIn(1f, UIPlaceText));
            other.gameObject.SendMessage("SetHomeBaseBool", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //StartCoroutine(FadeTextOut(1f, UIPlaceText));
            other.gameObject.SendMessage("SetHomeBaseBool", false);
        }
    }

    public IEnumerator FadeTextIn(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextOut(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
