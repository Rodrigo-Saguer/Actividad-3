using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("References")]
		[SerializeField] private GameObject m_enemy = null;

		[Header("Values")]
		[SerializeField] private float m_distance = 20f;
		[SerializeField] private float m_timeToGenerate = 0.5f;
        
        
    //Functions
	
		//MonoBehaviour Functions
		private void Start() {

			StartCoroutine(GenerateEnemyRoutine());
			}
        
        
		//Public Functions
        
        
		//Private Functions
        
        
	//Coroutines
	private IEnumerator GenerateEnemyRoutine() {

		while(true) {
			
			float m_angle = Random.Range(0, 360) * Mathf.Deg2Rad;
			Vector2 m_position = transform.position + new Vector3(Mathf.Cos(m_angle), Mathf.Sin(m_angle)) * m_distance;

			Instantiate(m_enemy, m_position, Quaternion.identity);
			yield return new WaitForSeconds(m_timeToGenerate);
			}
		}
	}
