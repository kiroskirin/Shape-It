using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ViewManager : MonoBehaviour
{

	enum Shape
	{
		NoShape,
		Square,
		Circle,
		Triangle
	}

	public GameObject squareShape;
	public GameObject circleShape;
	public GameObject triangleShape;
	public GameObject background;
	public Vector3 defaultPosition;
	public Canvas canvas;
	public GameObject colorPicker;
	public Text countText;
	public GameObject explosion;
	public float maxTouchCount;

	private Shape currentTap;
	private ColorPickerButton colorPickerButton;
	private List<string> tagsShape;
	private int touchCount;

	// Initial Objects
	void Start ()
	{
		touchCount = 0;
		currentTap = Shape.NoShape;

		tagsShape = new List<string> ();
		tagsShape.Add ("CircleShape");
		tagsShape.Add ("SquareShape");
		tagsShape.Add ("TriangleShape");

		GameObject tmpObj = GameObject.FindWithTag ("ColorPickerButton");
		if (tmpObj != null) {
			colorPickerButton = tmpObj.GetComponent<ColorPickerButton> ();
		}
		if (colorPickerButton == null) {
			Debug.Log ("Cannot find colorpicker script");
		}

		UpdateCount ();
	}

	void LateUpdate ()
	{
		CheckUserTapped ();
	}

	// Checking User Tab and Count
	void CheckUserTapped ()
	{
		foreach (Touch touch in Input.touches) {
			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				// if there is no tagged relate
				if (hit.collider.tag == "Untagged") {
					return;
				}
				// Count only when tap began
				if (touch.tapCount > 0 && touch.phase == TouchPhase.Began) {
					// Check if shape same as current active tap
					if (hit.collider.tag.Contains (currentTap.ToString ())) {
						touchCount += 1;
					}

					if (touchCount >= maxTouchCount) {
						MakeExplosion (hit);
					} else {
						// Update count
						UpdateCount ();	
					}
				}

			}

		}
	}

	void MakeExplosion (RaycastHit other)
	{
		Instantiate (explosion, other.transform.position, other.transform.rotation);
		Destroy (other.transform.gameObject);
		ResetCount();
	}

	// Update Count Text
	void UpdateCount ()
	{
		countText.text = "Count: " + touchCount;
	}

	// Reset Counter to 0
	void ResetCount ()
	{
		touchCount = 0;
		currentTap = Shape.NoShape;
		UpdateCount ();
	}

	// Generate Object when switch Tab bar
	public void GenerateObjectByType (Button sender)
	{
		if (sender.tag != currentTap.ToString ()) {
			// Reset Count Text
			ResetCount ();
			// Create new shape
			CreateShape (sender.tag);
		}
	}

	// Create shape by given name
	void CreateShape (string shape)
	{
		// Destroy all objects
		DestroyAllShape ();

		if (shape == Shape.Square.ToString ()) {
			Instantiate (squareShape, defaultPosition, Quaternion.identity);
			currentTap = Shape.Square;
		}

		if (shape == Shape.Circle.ToString ()) {
			Instantiate (circleShape, defaultPosition, Quaternion.identity);
			currentTap = Shape.Circle;
		}

		if (shape == Shape.Triangle.ToString ()) {
			Instantiate (triangleShape, defaultPosition, Quaternion.identity);
			currentTap = Shape.Triangle;
		}

	}

	// Destroy object except a given name
	void DestroyAllShape ()
	{
		foreach (string tag in tagsShape) {
			GameObject tmpObj = GameObject.FindWithTag (tag);
			if (tmpObj != null) {
				Destroy (tmpObj);
			}
		}
	}

	// Apply Color when select color picker
	public void ApplyColor (Button sender)
	{
		string currentShape = "NoShape";

		switch (currentTap) {
		case Shape.Circle:
			currentShape = "CircleShape";
			break;
		case Shape.Square:
			currentShape = "SquareShape";
			break;
		case Shape.Triangle:
			currentShape = "TriangleShape";
			break;
		default:
			colorPicker.gameObject.SetActive (false);
			return;
		}

		GameObject tmpObj = GameObject.FindWithTag (currentShape);
		if (tmpObj != null) {
			tmpObj.GetComponent<MeshRenderer> ().material.color = sender.GetComponent<Image> ().color;
			colorPickerButton.CloseColorPanel ();
		}
	}

}
