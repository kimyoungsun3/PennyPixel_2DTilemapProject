using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMaster<T> : FSM<T>, IDamageable {
	public System.Action<EnemyMaster<T>> callbackDeath;
	protected Vector3 hitPoint, hitDir;
	public Transform trans;
	public GameObject go;

	public float STARTING_HEALTH = 3;
	protected float health;
	protected bool bDead;
	public float viewDistance = 4f;
	public LayerMask mask;
	public float moveSpeed 	= 2f;
	public Vector3 moveDir;
	protected float damage = 1f;
	public ENEMY_AI_TYPE aiType = ENEMY_AI_TYPE.Patriot;

	//------------------------------------
	// 
	//------------------------------------
	public virtual void Awake(){
		trans 	= transform;
		go 		= gameObject;
	}

	//------------------------------------
	//
	//------------------------------------
	public virtual void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDir){
		health 		-= _damage;
		hitPoint 	= _hitPoint;
		hitDir 		= _hitDir;

		if (health <= 0f && !bDead) {
			Die ();
		}
	}

	public virtual void TakeDamage(float _damage){
		health -= _damage;

		if (health <= 0f && !bDead) {
			Die ();
		}
	}

	protected abstract void Die ();
	//{
	//	bDead = true;
	//	if (callbackDeath != null) {
	//		callbackDeath (this);
	//		callbackDeath = null;
	//	}
	//	Destroy ();
	//}

	public virtual void Destroy(){
		callbackDeath = null;
		gameObject.SetActive (false);
	}
}
