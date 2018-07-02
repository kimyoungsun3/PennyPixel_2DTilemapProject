using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_RightButton : MonoBehaviour {
	//public static Ui_RightButton ins;
	public VirtualButton btnJump;
	public VirtualButton btnAttack;
	public VirtualButton btnSkill1;
	public VirtualButton btnSkill2;

	//void Awake(){
	//	ins = this;
	//}
	public void SetActive2(bool _b){
		gameObject.SetActive (_b);
	}

	//-----------------------------
	// Jump
	//-----------------------------
	public bool GetJumpDown(){	
		return btnJump.GetButtonDown ();	
	}
	public bool GetJumpUp(){
		return btnJump.GetButtonUp ();
	}

	//Attack
	public bool GetAttackDown(){
		return btnAttack.GetButtonDown ();
	}

	//Skill1, 2
	public bool GetSkill1Down(){
		return btnSkill1.GetButtonDown ();
	}
	public bool GetSkill2Down(){
		return btnSkill2.GetButtonDown ();
	}
}
