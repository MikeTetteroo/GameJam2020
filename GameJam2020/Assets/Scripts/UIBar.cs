using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour
{
    public bool isHealthBar = true;
    public GameObject target;

    private float currentSize;
    private Status targetStatus;
    public Transform targetBar;

    void Start()
    {
        targetStatus = target.GetComponent<Status>();

        targetBar = transform.Find("Bar");
    }

    void Update()
    {
        // Get the healthbar of the target
        if (isHealthBar)
        {
            currentSize = targetStatus.currentHealth / targetStatus.defaultHealth;
        }

        // If it is not the healthbar, get the stamina instead
        else
        {
            currentSize = targetStatus.currentStamina / targetStatus.defaultStamina;
        }

        targetBar.localScale = new Vector3(currentSize, 1, 1);
    }
}
