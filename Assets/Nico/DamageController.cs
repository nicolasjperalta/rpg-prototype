using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nico{
	public class DamageController : MonoBehaviour {

        private HealthController m_healthCtrl;
        private Animator animController;
        private EnemyMovement m_enemyMovement;

		// Use this for initialization
		void Start () {
			m_healthCtrl = GetComponent<HealthController> ();
            animController = GetComponent<Animator>();
            m_enemyMovement = GetComponent<EnemyMovement>();
		}
		
		public void makeDamage(float ammount){
            if (m_healthCtrl.isAlive())
            {
                m_healthCtrl.substract(ammount);
                animController.SetTrigger("damage");

                if (!m_healthCtrl.isAlive())
                {
                    animController.SetTrigger("dead");
                    if(m_enemyMovement != null)
                        m_enemyMovement.enabled = false;
                }
              }
			
		}

    }
}
