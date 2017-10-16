using Nico;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {

    public float attackDelay;
    public int damage;

    private WaitForSeconds attackWait;
    private NavMeshAgent navAgent;
    private Animator animController;
    private GameObject target;
    private bool isAttacking;


    // Use this for initialization
    void Start () {
        attackWait = new WaitForSeconds(attackDelay);
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        isAttacking = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (isAttacking && target != null)
                faceTarget();
        }
    }

    public void attack(GameObject attackTarget) {
        if (!isAttacking)
        {
            StartCoroutine(makeAttack());
            isAttacking = true;
        }
        
        target = attackTarget;
    }

    IEnumerator makeAttack()
    {
        yield return null;

        animController.SetTrigger("attack");
        navAgent.Stop();

        while (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            if(target != null)
                target.GetComponent<DamageController>().makeDamage(damage);

            yield return attackWait;
        }

        navAgent.Resume();
        animController.SetTrigger("run");
        isAttacking = false;

        yield break;
    }

    void faceTarget()
    {
        gameObject.transform.LookAt(target.transform);
    }
}
