using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBrain : MonoBehaviour {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
		private static PlayerBrain m_instance;
        
		//Non Static
		[Header("References")]
		[SerializeField] private SpriteRenderer m_spriteRenderer = null;
		[SerializeField] private Rigidbody2D m_rigidbody = null;
		[SerializeField] private Transform m_light = null;
		[SerializeField] private GameObject m_bullet = null;
		
		[Header("Movement")]
		[SerializeField] private float m_speed = 4;
		[SerializeField] private float m_smoothness = 0.1f;
		
		[Header("Animation")]
		[SerializeField] private Sprite[] m_frontAnimation = null;
		[SerializeField] private Sprite[] m_leftAnimation = null;
		[SerializeField] private Sprite[] m_backAnimation = null;
		[SerializeField] private Sprite[] m_rightAnimation = null;
		[SerializeField] private float m_spriteTimeBetweenFrames = 0.012f;

		[Header("Target")]
		[SerializeField] private Transform m_target = null;
		[SerializeField] private float m_targetLerp = 0.2f;

        
		//Animation
		private Coroutine m_animationCoroutine = null;
		private bool m_animate = false;
		private Sprite[] m_selectedAnimation;

		//Velocity
		private Vector2 m_velocity = Vector2.zero;
		private Vector2 m_inputVelocity = Vector2.zero;
		private Vector2 m_smoothDampVelocity = Vector2.zero;

		//Direction
		private Vector2 m_lookValue = Vector2.zero;
		private float m_directionAngle = 0;

		//References
		private Camera m_camera = null;

		//Light
		private float m_lightMultiplier = 1;
		private float m_lightVelocity = 0;

    //Functions
	
		//MonoBehaviour Functions
        private void Awake() {

			m_instance = this;
			}
		private void Start() {
			
			m_selectedAnimation = m_frontAnimation;
			m_animationCoroutine = StartCoroutine(SpriteAnimation());

			StartCoroutine(LightAnimation());
			
			m_camera = CameraBrain.GetSingleton().GetCamera();
			}
        private void Update() {
			
			//Movement
			m_velocity = Vector2.SmoothDamp(m_velocity, m_inputVelocity, ref m_smoothDampVelocity, m_smoothness, Mathf.Infinity);
			m_animate = Mathf.Abs(m_velocity.magnitude) > 0.5f;

			//Direction
			Vector2 m_mousePosition = m_camera.ScreenToWorldPoint(m_lookValue);
			m_directionAngle = Mathf.Atan2(m_mousePosition.y - transform.position.y, m_mousePosition.x - transform.position.x) * Mathf.Rad2Deg;

			if (m_directionAngle > -45 && m_directionAngle <= 45) m_selectedAnimation = m_rightAnimation;
			else if (m_directionAngle > 45 && m_directionAngle <= 135) m_selectedAnimation = m_backAnimation;
			else if (m_directionAngle > -135 && m_directionAngle <= -45) m_selectedAnimation = m_frontAnimation;
			else m_selectedAnimation = m_leftAnimation;

			//Light
			m_light.localScale = Vector3.one * Mathf.SmoothDamp(m_light.localScale.x, m_lightMultiplier, ref m_lightVelocity, 0.1f, Mathf.Infinity);

			//Target
			m_target.position = Vector2.Lerp(transform.position, m_mousePosition, m_targetLerp);
			}
		private void FixedUpdate() {

			m_rigidbody.velocity = m_velocity;
			}
		private void OnTriggerEnter2D(Collider2D other) {
			
			if (other.CompareTag("Enemy"))
				SceneManager.LoadScene(0);
			}

		//Public Functions
        public void OnMove(InputAction.CallbackContext context) => m_inputVelocity = context.ReadValue<Vector2>() * m_speed;
        public void OnLook(InputAction.CallbackContext context) => m_lookValue = context.ReadValue<Vector2>();
		public void OnShoot(InputAction.CallbackContext context) {
			
			if (!context.started) return;

			BulletBrain m_bulletBrain = Instantiate(m_bullet, transform.position, Quaternion.identity).GetComponent<BulletBrain>();
			m_bulletBrain.Create(m_directionAngle);
			}

		public static PlayerBrain GetSingleton() => m_instance;
			
		//Private Functions
        
        
	//Coroutines
	private IEnumerator SpriteAnimation() {

		while(true) {
			
			for(int i = 0; i < m_selectedAnimation.Length; i ++) {

				m_spriteRenderer.sprite = m_selectedAnimation[i];
				yield return new WaitForSeconds(m_spriteTimeBetweenFrames);
				}

			while(!m_animate) {

				m_spriteRenderer.sprite = m_selectedAnimation[0];
				yield return null;
				}
			}
		}
	private IEnumerator LightAnimation() {

		while(true) {

			m_lightMultiplier = Random.Range(0.9f, 1.1f);
			yield return new WaitForSeconds(0.2f);
			}
		}
	}
