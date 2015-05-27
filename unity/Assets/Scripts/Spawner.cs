using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject item_prefab;
	public float radius;
	public float height;
	public float delay;
	
	void Start () {
		StartCoroutine ("Spawn");
	}

	IEnumerator Spawn() {
		while (true) {
			Vector2 point = Random.insideUnitCircle * radius;
			GameObject item = Instantiate (item_prefab, new Vector3 (point.x, height, point.y), Quaternion.identity) as GameObject;
			yield return new WaitForSeconds(delay);
			Destroy (item);
		}
	}

}
