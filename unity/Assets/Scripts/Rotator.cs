using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	private Collider c;

	void Start() {
		c = GetComponent<Collider> ();
	}
	
	void Update() {
		transform.RotateAround(c.bounds.center, new Vector3 (15, 30, 45), 45 * Time.deltaTime);
	}
}
