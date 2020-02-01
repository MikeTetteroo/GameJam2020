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
        Debug.Log("ASS&TIDDIES");
        foreach (Transform collectible in collectibles)
        {
            if(collectible.name == collectibleName)
            {
                Debug.Log("Found ass?");
                collectible.gameObject.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeTextIn(1f, UIPlaceText));
            player.atDeposit = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeTextOut(1f, UIPlaceText));
            player.atDeposit = false;
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
