using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffect : MonoBehaviour
{
    public ParticleSystem PickUpFirework;
    public Movement player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible" && !player.holdingCollectible)
        {
            player.holdingCollectible = true;
            Firework(other);
            player.currentCollectible = other.name;
            Destroy(other.gameObject);
        }
    }

    void Firework(Collider other)
    {
        ParticleSystem clone = Instantiate(PickUpFirework, other.transform.position, Quaternion.identity);
    }
}
