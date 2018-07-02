using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Result : MonoBehaviour {
	GameObject go;
	Text msg;

	void Awake(){
		go = gameObject;
		msg = GetComponent<Text> ();

		go.SetActive (false);
	}

	public void SetActive2(bool _b){
		if (go == null) {
			go = gameObject;
			go.SetActive (true);
		}
		gameObject.SetActive (_b);
	}

	public void SetMessage(string _msg){
		msg.text = _msg;
	}

	//public void InvokeAgain(){
	//	go.SetActive (false);
	//	GameManager.ins.Restart ();
	//}
}
