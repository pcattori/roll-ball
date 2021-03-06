﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float bounciness;

	private Rigidbody rb;
	private Vector3 velocity;
	private bool grounded;
	private bool canJump;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		grounded = true;
		canJump = false;
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis (rb.tag + "Horizontal");
		float moveVertical = Input.GetAxis (rb.tag + "Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (speed * movement);
		velocity = rb.velocity;

		if (grounded && canJump && Input.GetKeyDown("space")) {
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

			rb.velocity = bounciness * (2f * v_cm - v_1);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 11) {
			Destroy(other.gameObject);
			StartCoroutine("PickUpJump");
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

	IEnumerator PickUpJump() {
		canJump = true;
		yield return new WaitForSeconds(10);
		canJump = false;
	}
}
