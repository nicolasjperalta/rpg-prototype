using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nico {
	public class DamageController : MonoBehaviour {
		HealthController m_healthCtrl;

		// Use this for initialization
		void Start () {
			m_healthCtrl = GetComponent<HealthController> ();
		}
		
		void makeDamage(float ammount){
			m_healthCtrl.subtract (ammount);
		}

	}
}