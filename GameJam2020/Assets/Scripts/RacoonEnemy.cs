using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RacoonEnemy : MonoBehaviour
{
    private Vector3 startPosition;
    private float wanderSpeed = 0.5f;
    private float chaseSpeed = 3f;
    public GameObject target;

    void Awake()
    {
        startPosition = this.transform.position;
        InvokeRepeating("Wander", 1f, 5f);
    }

    void Wander()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderSpeed;

        Vector3 destination = startPosition + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        NewDestination(destination);
    }

    void Chase()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;

        Vector3 destination = target.transform.position;
        NewDestination(destination);

        float dist = Vector3.Distance(agent.transform.position, target.transform.position);
        if (dist >= 15)
        {
            CancelInvoke();
            InvokeRepeating("Wander", 1f, 5f);
        }
    }

    public void NewDestination(Vector3 targetPoint)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CancelInvoke();
            InvokeRepeating("Chase", 0.5f, 1f);
        }   
    }
}
