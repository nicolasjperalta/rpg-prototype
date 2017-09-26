using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nico {
	
	public class HealthController : MonoBehaviour {

		[SerializeField] float Health;

		public void subtract(float ammount){
			Health -= ammount;
		}

		bool isAlive(){
			return Health > 0;
		}


	}
}