using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour
{
    public GameObject target;

    private float currentSize;
    public Status targetStatus;
    public Transform targetBar;

    void Start()
    {
        //targetStatus = target.GetComponent<Status>();
        targetBar = transform.Find("Bar");
    }

    void Update()
    {
        // Get the healthbar of the target
        currentSize = targetStatus.currentHealth / targetStatus.defaultHealth;

        targetBar.localScale = new Vector3(currentSize, 1, 1);
    }

    public void GetUIComponents()
    {
        targetStatus = target.GetComponent<Status>();
        targetBar = transform.Find("Bar");
    }
}
