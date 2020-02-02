using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaCircle : MonoBehaviour
{
    public GameObject target;

    private float currentSize;
    public Status targetStatus;
    private Image imageComponent;

    void Start()
    {
        //targetStatus = target.GetComponent<Status>();
        imageComponent = GetComponent<Image>();
    }

    void Update()
    {
        // Get the stamina of the target
        currentSize = targetStatus.currentStamina / targetStatus.defaultStamina;

        if (currentSize == 1)
        {
            imageComponent.fillAmount = 0;
        }
        else
        {
            imageComponent.fillAmount = currentSize;
        }
    }

    public void GetUI()
    {
        Debug.Log("titties");
        targetStatus = target.GetComponent<Status>();
        imageComponent = GetComponent<Image>();
    }
}
