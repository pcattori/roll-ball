using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	private Collider c;

	void Start() {
		c = GetComponent<Collider> ();
	}
	
	void Update() {
		//transform.Rotate(new Vector3 (0, 90, 0) * Time.deltaTime);
		transform.RotateAround(c.bounds.center, new Vector3 (15, 30, 45), 45 * Time.deltaTime);
	}
}
