using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrain : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        private static CameraBrain m_instance = null;
        
		//Non Static
        [Header("References")]
		[SerializeField] private Camera m_camera = null;
        [SerializeField] private Transform m_target = null;

		[Header("Movement")]
		[SerializeField] private float m_smoothness = 0.1f;
		
		private Vector2 m_smoothDampVelocity = Vector2.zero;
		private Vector2 m_targetPosition = Vector2.zero;

    //Functions
	
		//MonoBehaviour Functions
        private void Awake() {

			m_instance = this;
			m_targetPosition = m_target.position;
			}
		private void Update() {

			m_targetPosition = Vector2.SmoothDamp(m_targetPosition, m_target.position, ref m_smoothDampVelocity, m_smoothness, Mathf.Infinity);
			transform.position = new Vector3(m_targetPosition.x, m_targetPosition.y, -10);
			}
        
		//Public Functions
        public static CameraBrain GetSingleton() => m_instance;
		public Camera GetCamera() => m_camera;
        
		//Private Functions
        
        
	//Coroutines
	
	}
