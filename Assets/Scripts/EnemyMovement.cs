using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public Transform spawnPoint;
    public int walkableRadio; //this variable indicates the max distance the enemy can walk away from it's spawn point
    public float aggroRadio;
    public float runSpeed;
    public float runAngularSpeed;
    public float attackDelay;
    public float attackRange;

    private bool isPursuing;  //indicates if the enemy is pursuing the character or it can walk freely
    private bool isWalking;
    private bool isAttacking;

    private float direction;
    private float distance;
    private float walkSpeed;
    private float walkAngularSpeed;

    private WaitForSeconds walkWait;
    private WaitForSeconds attackWait;
    private Vector3 destination;
    private NavMeshAgent navAgent;
    private Animator animController;
    private SphereCollider aggroRange;
    private GameObject target;

    static WaitForSeconds updateDelay = new WaitForSeconds(.5f); //delay before checking the player position again


	// Use this for initialization
	void Start () {
        isPursuing = false;
        isWalking = false;
        isAttacking = false;

        walkWait = new WaitForSeconds(10);
        attackWait = new WaitForSeconds(attackDelay);
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        StartCoroutine(getRandomDestination());
        aggroRange = GetComponent<SphereCollider>();

        aggroRange.radius = aggroRadio;
        walkSpeed = navAgent.speed;
        walkAngularSpeed = navAgent.angularSpeed;

    }
	
	// Update is called once per frame
	void Update () {
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (isWalking)
            {
                animController.SetTrigger("idle");
                isWalking = false;
            }

            if (isPursuing && !isAttacking)
                StartCoroutine(attack());

            if (isAttacking)
                faceTarget();

        }
        

	}

    void startPursue(Collider player)
    {
        isWalking = false;
        isPursuing = true;

        target = player.gameObject;
        navAgent.speed = runSpeed;
        navAgent.angularSpeed = runAngularSpeed;
        animController.SetTrigger("run");

        StartCoroutine(pursueTarget());
    }

    void faceTarget()
    {
        gameObject.transform.LookAt(target.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
           startPursue(other);
    }

    IEnumerator getRandomDestination()
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

                animController.SetTrigger("walk");
                isWalking = true;
                
            }

            yield return walkWait;
        }
        
    }

    IEnumerator pursueTarget()
    {
        while (isPursuing)
        {
            navAgent.destination = target.transform.position;

            yield return updateDelay;
        }

        navAgent.speed = walkSpeed;
        navAgent.angularSpeed = walkAngularSpeed;

        yield break;
    }

    IEnumerator attack()
    {
        animController.SetTrigger("attack");
        navAgent.Stop();

        isAttacking = true;

        while (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            //make the attack and aply damage
            yield return attackDelay;
        }

        navAgent.Resume();
        animController.SetTrigger("run");
        isAttacking = false;

        yield break;
    }

}
