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
	public GameObject background;
	public Vector3 defaultPosition;
	public Canvas canvas;
	public GameObject colorPicker;

	private bool isShapeCreated;
	private Shape currentTap;

	private ColorPickerButton colorPickerButton;

	private List<string> tagsShape;

	void Start ()
	{
		currentTap = Shape.NoShape;
		isShapeCreated = false;

		tagsShape = new List<string> ();
		tagsShape.Add ("CircleShape");
		tagsShape.Add ("SquareShape");
		tagsShape.Add ("TriangleShape");

		GameObject tmpObj = GameObject.FindWithTag ("ColorPickerButton");
		if (tmpObj != null) {
			colorPickerButton = tmpObj.GetComponent<ColorPickerButton> ();
		}
		if (colorPickerButton == null) {
			Debug.Log("Cannot find colorpicker script");
		}
	}

	public void GenerateObjectByType (Button sender)
	{
		if (sender.tag != currentTap.ToString ()) {
			isShapeCreated = false;
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

		isShapeCreated = true;
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
			colorPickerButton.CloseColorPanel();
		}
	}

}
