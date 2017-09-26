using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	[SerializeField] float Health;

	public void substract(float ammount){
		Health -= ammount;
		if (!isAlive ()) {
			Destroy (gameObject);
		}
	}

	bool isAlive(){
		return Health > 0;
	}

}
