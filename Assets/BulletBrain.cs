using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBrain : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("Values")]
        [SerializeField] private float m_speed = 16f;
		
		private float m_direction;
        
    //Functions
	
		//MonoBehaviour Functions
        private void Update() {
			
			transform.position += new Vector3(Mathf.Cos(m_direction), Mathf.Sin(m_direction)) * m_speed * Time.deltaTime;
			if (Mathf.Abs(transform.position.x) > 40 || Mathf.Abs(transform.position.y) > 40) Destroy(gameObject); 
			}
        
		//Public Functions
		public void Create(float angle) {

			m_direction = angle * Mathf.Deg2Rad;
			transform.eulerAngles = new Vector3(0, 0, angle);
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}
