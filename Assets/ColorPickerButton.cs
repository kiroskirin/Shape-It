using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorPickerButton : MonoBehaviour {

	public Button colorPickerButton;
	public GameObject colorPickerPanel;

	private bool isOpen = false;

	void Start () {
		colorPickerPanel.SetActive (false);
	}
	
	void Update () {
	
	}

	public void ToggleColorPanel() {
		if (isOpen)
			CloseColorPanel ();
		else
			OpenColorPanel ();
	}

	private void OpenColorPanel() {
		colorPickerPanel.SetActive (true);
		isOpen = !isOpen;
		Time.timeScale = 0;
	}

	private void CloseColorPanel() {
		colorPickerPanel.SetActive (false);
		isOpen = !isOpen;
		Time.timeScale = 1;
	}
}
