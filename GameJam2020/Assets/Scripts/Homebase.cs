using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Homebase : MonoBehaviour
{
    public Text UIPlaceText;
    public GameObject[] collectibles;
    public Collider deliverPoint;
    // Start is called before the first frame update

    public void PlaceCollectible(string collectibleName)
    {
        foreach (GameObject collectible in collectibles)
        {
            if(collectible.name == collectibleName)
            {
                collectible.SetActive(true);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(TextFade(true));
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(TextFade(false));
        }
    }

    public IEnumerator TextFade(bool fadeIn)
    {
        yield return new WaitForSeconds(0.2f);
    }
}
