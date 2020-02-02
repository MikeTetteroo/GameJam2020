using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] points;
    public Transform player;
    public bool aggressive;
    public float aggroRange;
    public float AttackRange;
    private bool isAttacking;
    private int destPoint = 0;
    private float attackDamage = 10;
    private UnityEngine.AI.NavMeshAgent agent;


        void Start () {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            // Disabling auto-braking allows for continuous movement
            // between points (ie, the agent doesn't slow down as it
            // approaches a destination point).
            agent.autoBraking = false;

            GotoNextPoint();
        }


        void GotoNextPoint() {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }


        void Update () {

            if(aggressive && Vector3.Distance(player.position, transform.position) < aggroRange)
            {
                agent.destination = player.position;
            }
            // Choose the next destination point when the agent gets
            // close to the current one.
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        

        if (Vector3.Distance(player.position, transform.position) < AttackRange){
             Attacking(player.gameObject);
        }


        IEnumerator Attacking (GameObject player) {
            if (!isAttacking) {
                isAttacking = true;
                player.GetComponent<Status>().currentHealth =- attackDamage;
                yield return new WaitForSeconds(1f);
                isAttacking = false;
            }
    }

    }




}

