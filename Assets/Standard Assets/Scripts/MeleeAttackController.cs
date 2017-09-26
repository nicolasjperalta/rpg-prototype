using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour {
	CapsuleCollider m_Capsule;
	// Use this for initialization
	void Start () {
		m_Capsule = GetComponent<CapsuleCollider>();
	}
	public void Attack(){
		Debug.Log ("Attack started");
		RaycastHit hit;
		Vector3 p1 = transform.position + m_Capsule.center;

		if (Physics.SphereCast (p1, m_Capsule.height / 2, transform.forward, out hit,2)) {
			HealthController healthCtrl = hit.collider.gameObject.GetComponent<HealthController> ();
			healthCtrl.substract (20);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
