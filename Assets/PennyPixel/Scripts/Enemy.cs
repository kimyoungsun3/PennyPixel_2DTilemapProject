using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolManager5;

public class Enemy : EnemyMaster<ENEMY_STATE> {
	//------------------------------------
	//	
	//------------------------------------
	//public override void Awake(){
	//
	//}

	void Start(){
		AddState(ENEMY_STATE.Patriot, 	pInPatriot, 	ModifyPatriot, 	null);
		AddState(ENEMY_STATE.Chase, 	pInChase, 		ModifyChase, 	null);

		InitFirst ();
		MoveState(ENEMY_STATE.Patriot);
	}

	public void InitFirst(){
		health 	= STARTING_HEALTH;
		bDead 	= false;
		moveDir	= Constant.right;
	}

	//------------------------------------
	//Damage -> TakeHit, TakeDamage -> Die
	//------------------------------------
	protected override void Die(){
		bDead = true;


		//Expire Effect
		PoolMaster _p = PoolManager.ins.Instantiate("EffectEnemyDeath", hitPoint, Quaternion.identity).GetComponent<PoolMaster>();
		_p.Play ();

		//Sound
		//SoundManager.ins.Play ("Enemy attack", false);

		//Message Show
		//Ui_MsgRoot.ins.InvokeShowMessage("UiHitMessage", "[00ff00]Hit[-]", hitPoint, 5f);

		//등록된 콜백사용...
		if (callbackDeath != null) {
			callbackDeath (this);
			callbackDeath = null;
		}

		Destroy ();
	}

	//-----------------------------
	// Patriot
	//-----------------------------
	void pInPatriot(){
		//...
	}

	void ModifyPatriot(){
		//유저 검색...
		//if(Physics2D.Raycast(transHead.position, )){
		//}
		Debug.Log(Physics2D.gravity);

		//이동...
		trans.Translate (moveDir * moveSpeed * Time.deltaTime);
	}

	//-----------------------------
	// Chase
	//-----------------------------
	void pInChase(){

	}

	void ModifyChase(){

	}

	void OnDrawGizmos(){
		//Search Area
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position + Vector3.one/2f, transform.right * viewDistance);
		Gizmos.DrawWireSphere(transform.position, viewDistance);
	}

}
