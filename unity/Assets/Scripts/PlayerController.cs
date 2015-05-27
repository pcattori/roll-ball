using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;
	private Vector3 velocity;
	private bool grounded;
	private bool jump;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		grounded = true;
		jump = false;
	}

	void Update() {
		if (grounded && Input.GetKeyDown ("space")) {
			grounded = false;
			jump = true;
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis (rb.tag + "Horizontal");
		float moveVertical = Input.GetAxis (rb.tag + "Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (speed * movement);
		velocity = rb.velocity;

		if (grounded && Input.GetKeyDown("space")) {
			rb.velocity += new Vector3(0f, 10f, 0f);
		}
		grounded = false;
	}

	void OnCollisionEnter(Collision other) {
		// Elastic collision: v_i' = 2v_cm - v_i
		if (other.gameObject.layer == 8) { // other players
			float m_1 = rb.mass;
			float m_2 = other.gameObject.GetComponent<Rigidbody> ().mass;

			Vector3 v_1 = velocity;
			Vector3 v_2 = velocity + other.relativeVelocity;

			Vector3 v_cm = (m_1 * v_1 + m_2 * v_2) / (m_1 + m_2);

			rb.velocity = 2f * v_cm - v_1;
		}
	}

	void OnCollisionStay(Collision other) {
		if(other.gameObject.layer == 9 && other.contacts.Length > 0) {
			var contactPoints = other.contacts;
			if((Vector3.Dot(contactPoints[0].normal, Vector3.up)) > 0.5){ //Check for floor and upwards
				grounded = true;
			}
		}
	}
}
