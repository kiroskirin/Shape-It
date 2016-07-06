using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	// Use this for initialization
	public float tumble;

	void Update () {
		transform.Rotate(Random.insideUnitSphere * tumble);
	}
	
}
