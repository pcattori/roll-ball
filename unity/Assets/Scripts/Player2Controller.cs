using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour {
	
	private Rigidbody rb;
	private Vector3 velocity;
	public float speed;
	
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Player2Horizontal");
		float moveVertical = Input.GetAxis("Player2Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce(speed * movement);

		velocity = rb.velocity;
	}

	void OnCollisionEnter(Collision other) {
		// use mass ratio to compute velocity transform
		//this.transform.GetComponent<Rigidbody>().velocity = Vector3.Reflect(other.relativeVelocity*-1, other.contacts[0].normal );
		/*int bounciness = 1;
		
		Vector3 normal = Vector3.zero;
		foreach(ContactPoint c in collision.contacts) {
			normal += c.normal;
		}
		normal.Normalize();
		Vector3 newVelocity = bounciness * (-2f * (Vector3.Dot(velocity,normal) * normal) + velocity);
		rb.velocity = newVelocity;
		*/

		float m_1 = rb.mass;
		float m_2 = other.gameObject.GetComponent<Rigidbody>().mass;
		
		Vector3 v_1 = velocity;
		Vector3 v_2 = velocity + other.relativeVelocity;
		
		Vector3 v_cm = (m_1 * v_1 + m_2 * v_2) / (m_1 + m_2);
		
		rb.velocity = 2f * v_cm - v_1;
	}
}
