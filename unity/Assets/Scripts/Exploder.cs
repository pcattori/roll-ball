using UnityEngine;
using System.Collections;

public class Exploder : MonoBehaviour {
	
	public int power;
	public float radius;
	public float countdown;
	
	void Start() {
		StartCoroutine ("Countdown");
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 8) { // players
			Explode ();
		}
	}

	IEnumerator Countdown() {
		yield return new WaitForSeconds(countdown);
		Explode ();
	}

	void Explode() {
		var explosionPos = transform.position;
		var colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			if (hit.gameObject.layer == 8) { // players
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if (rb != null) {
					rb.AddExplosionForce(power, explosionPos, radius, 0.35F, ForceMode.Impulse);
				}
			}
		}
		Destroy (gameObject);
	}
}
