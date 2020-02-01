using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffect : MonoBehaviour
{
    public ParticleSystem PickUpFirework;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            Firework(other);
        }
    }

    void Firework(Collider other)
    {
        ParticleSystem clone = Instantiate(PickUpFirework, other.transform.position, Quaternion.identity);
    }
}
