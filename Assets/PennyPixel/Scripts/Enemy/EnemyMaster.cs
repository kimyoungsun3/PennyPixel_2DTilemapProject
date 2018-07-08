using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMaster : MonoBehaviour, IDamageable {
	[HideInInspector]public Transform trans;
	[HideInInspector]public GameObject go;
	[HideInInspector]public System.Action<EnemyMaster> callbackDeath;
	[HideInInspector]public Vector3 hitPoint, hitDir;

	public float STARTING_HEALTH = 3;
	public LayerMask mask;
	public float viewDistance = 4f;
	public float moveSpeed 	= 2f;
	[HideInInspector]public Vector3 moveDir;
	[HideInInspector]public float damage = 1f;
	[HideInInspector]public float health;
	[HideInInspector]public bool bDead;

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

	protected virtual void Die ()
	{
		bDead = true;
		if (callbackDeath != null) {
			callbackDeath (this);
			callbackDeath = null;
		}
		Destroy ();
	}

	public virtual void Destroy(){
		callbackDeath = null;
		gameObject.SetActive (false);
	}
}
