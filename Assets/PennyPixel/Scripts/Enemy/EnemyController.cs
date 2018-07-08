using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolManager5;
using SO;

public class EnemyController : EnemyMaster {
	public State stateCurrent, stateRemain;
	//public Projectile bullet;
	public Vector3[] wayLocal;
	[HideInInspector] public Vector3[] wayWorld;
	[HideInInspector] public int wayIndex;
	[HideInInspector] public Transform target;
	[HideInInspector] public SpriteRenderer spriteRenderer;
	float nextTime;


	public float attackDistance = 3f;
	public float attackRate = 1f;
	public float chaseDistance = 3f;
	public float speedChase = 3f;
	public float seeDistance = 6;
	public float chaseLookOnTime = 2f;

	public Vector2 viewDir;
	Vector3 beforePos;

	//------------------------------------
	//	
	//------------------------------------
	public override void Awake(){
		base.Awake ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start(){
		InitFirst ();
	}

	public void InitFirst(){
		health 	= STARTING_HEALTH;
		bDead 	= false;
		moveDir	= Constant.right;
		wayWorld = new Vector3[wayLocal.Length];
		for (int i = 0; i < wayLocal.Length; i++) {
			wayWorld [i] = trans.position + wayLocal [i];
		}
		beforePos = trans.position;
	}
	//-----------------------------
	// Ai Update
	//-----------------------------
	void Update () {
		if (stateCurrent != null) {
			//Debug.Log (this + ":Update:");
			stateCurrent.UpdateState (this);
		}

		if (beforePos != trans.position) {
			SetDirAndSprite ();
		}
		beforePos = trans.position;
	}

	//dir -> flipX
	private void SetDirAndSprite()
	{
		Vector3 _dir = trans.position - beforePos;
		float _dirSign1 = Mathf.Sign(_dir.x);
		float _dirSign2 = Mathf.Sign(viewDir.x);

		//Debug.Log (string.Format("{0} {1} {2} {3}", viewDir, _dirSign2, _dir, _dirSign1 ));
		if (_dirSign1 != _dirSign2) 
		{
			//Debug.Log (" > " + (_dirSign1 < 0));
			spriteRenderer.flipX =  (_dirSign1 < 0);
		}
		viewDir = _dir;
	}

	public void SetState(State _nextState){
		if(_nextState != stateRemain){
			stateCurrent = _nextState;
			InitTime ();
		}
	}

	public void InitTime(){
		nextTime 		= 0;
		nextShootTime 	= 0;
	}

	public void SetTargeting(Transform _target){
		target = _target;
		lockOnTime = 0;
	}

	public bool CheckLockOnTime(float _duration){
		lockOnTime += Time.deltaTime;
		return (lockOnTime < _duration);
	}

	float nextShootTime, lockOnTime;
	public void Shoot(){
		//Debug.Log (this + ":Shoot:" + Time.time + ":" + nextShootTime);
		if (Time.time > nextShootTime) {
			//Debug.Log (" > ");
			lockOnTime = 0;
			nextShootTime = Time.time + attackRate;
			PlayerBullet2 _scp = PoolManager.ins.Instantiate ("EnemyBullet", trans.position, Quaternion.identity).GetComponent<PlayerBullet2>();
			_scp.InitFirst (spriteRenderer.flipX?0:1, 1f);
		}
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
	// Editor mode
	//-----------------------------
	//#if UNITY_EDITOR
	//void OnDrawGizmos(){
	//	//Search Area
	//	if (stateCurrent != null && trans != null) {
	//		//Debug.Log (stateCurrent.color);
	//		Gizmos.color = stateCurrent.gizmosColor;
	//		Gizmos.DrawWireCube (trans.position, trans.localScale * 1.5f);
	//		//Gizmos.DrawRay (trans.position, trans.forward * 5f);
	//		//Debug.DrawRay (transform.position, transform.forward * 5f, Color.white);
	//	}
	//}
	//#endif

}
