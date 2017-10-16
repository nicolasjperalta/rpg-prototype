using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	[SerializeField] float Health;

    public void substract(float ammount){
		Health -= ammount;
		if (!isAlive ()) {
            StartCoroutine(die());
		}
	}

	public bool isAlive(){
		return Health > 0;
	}

    IEnumerator die()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);

        yield break;
    }

}
