using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{

    public Text collectibleDeliver;
    public Text returnCollectible;
    public StaminaCircle stamCircle;
    public UIBar uiBar;
    public static UiScript Instance { get; private set; }

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

    public void GetUIComponents()
    {
        Debug.Log("ass");
        StartCoroutine(CollectComponents());
    }

    public void FadeText(bool fadeIn)
    {
        if(fadeIn)
            StartCoroutine(FadeTextIn(1f, collectibleDeliver));
        else
            StartCoroutine(FadeTextOut(1f, collectibleDeliver));
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

    public void ReturnItem()
    {
        StartCoroutine(ReturnCollectible(1f, returnCollectible));
    }

    public IEnumerator ReturnCollectible(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
        }
        yield return new WaitForSeconds(4f);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator CollectComponents()
    {
        Debug.Log("tiddies");
        yield return new WaitForSeconds(2f);
        //stamCircle = GetComponent<StaminaCircle>();
        //uiBar = GetComponent<UIBar>();
        yield return new WaitForSeconds(2f);
        uiBar.GetUIComponents();
        stamCircle.GetUI();
    }
}
