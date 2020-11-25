using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("References")]
		[SerializeField] private Rigidbody2D m_rigidbody = null;

		[Header("Movement")]
		[SerializeField] private float m_speed = 8;
		[SerializeField] private float m_smoothness = 0.1f;

		private Transform m_playerTransform = null;
		private Vector2 m_smoothDampVelocity = Vector2.zero; 
        
        
    //Functions
	
		//MonoBehaviour Functions
        private void Start() {

			m_playerTransform = PlayerBrain.GetSingleton().transform;
			}
		private void OnTriggerEnter2D(Collider2D other) {
			
			if (other.CompareTag("Bullet")) {
				
				Destroy(other.gameObject);
				Destroy(gameObject);
				}
			}
        
		//Public Functions
		private void Update() {

			Vector2 m_velocity = (m_playerTransform.transform.position - transform.position).normalized;
			m_rigidbody.velocity = Vector2.SmoothDamp(m_rigidbody.velocity, m_velocity * m_speed, ref m_smoothDampVelocity, m_smoothness, Mathf.Infinity);
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
