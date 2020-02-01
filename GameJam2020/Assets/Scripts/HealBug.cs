using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBug : MonoBehaviour
{
    public float healAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<Status>().GainHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
