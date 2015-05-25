﻿using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour {

	private Rigidbody rb;
	public float speed;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Player1Horizontal");
		float moveVertical = Input.GetAxis("Player1Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(speed * movement);
	}
}
