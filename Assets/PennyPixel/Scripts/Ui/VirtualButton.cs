using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	bool bPressed, bRelease;

	public void OnPointerDown(PointerEventData e) {
		//Debug.Log(this + ":OnPointerDown:" + e);
		bPressed = true;
		bRelease = false;
	}

	public void OnPointerUp(PointerEventData e) {
		//Debug.Log(this + ":OnPointerUp:" + e);
		bPressed = false;
		bRelease = true;
	}

	public bool GetButtonDown(){
		return bPressed;
	}

	public bool GetButtonUp(){
		return bRelease;
	}
}
