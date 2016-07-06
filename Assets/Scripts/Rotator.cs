using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	public Vector3 rotate;

	void Update ()
	{
		transform.Rotate (rotate * Time.deltaTime);	
	}
}
