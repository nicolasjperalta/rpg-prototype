using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public Transform spawnPoint;
    public int walkableRadio; //this variable indicates the max distance the enemy can walk away from it's spawn point
    public float stopDistance;

    private bool isPursuing;  //indicates if the enemy is pursuing the character or it can walk freely
    private bool isWalking;

    private float direction;
    private float distance;
    private WaitForSeconds walkWait;
    private Vector3 destination;
    private NavMeshAgent navAgent;
    private Animator animController;


	// Use this for initialization
	void Start () {
        isPursuing = false;
        isWalking = false;
        walkWait = new WaitForSeconds(10);
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        StartCoroutine(setDestination());

    }
	
	// Update is called once per frame
	void Update () {
        if(navAgent.remainingDistance < 0.1)
        {
            navAgent.updateRotation = false;
            if (isWalking)
            {
                animController.SetTrigger("idle");
                isWalking = false;
            }

            
        }
	}

    IEnumerator setDestination()
    {
        yield return new WaitForSeconds(2);

        while (true)
        {
            if (!isPursuing && !isWalking)
            {
                direction = Random.Range(0, 360); //obtains a random angle for movement direction
                distance = Random.Range(0, walkableRadio);

                float destinationX = spawnPoint.position.x + distance * Mathf.Cos(direction * Mathf.Deg2Rad);
                float destinationZ = spawnPoint.position.z + distance * Mathf.Sin(direction * Mathf.Deg2Rad);

                destination = new Vector3(destinationX, 0, destinationZ);
                navAgent.destination = destination;
                navAgent.updateRotation = true;
                               

                animController.SetTrigger("walk");
                isWalking = true;
                
            }

            yield return walkWait;
        }
        
    }

}
