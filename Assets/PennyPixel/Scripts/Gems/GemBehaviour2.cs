using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolManager5;

public class GemBehaviour2 : MonoBehaviour
{
	Transform trans;

	void Awake(){
		trans = transform;
	}

	void OnTriggerEnter2D(Collider2D _col)
	{
		if (_col.CompareTag ("Player")) {
			OnHitObject (_col, trans.position);
		}
	}

	void OnHitObject(Collider2D _col, Vector3 _hitPoint){
		//Debug.Log (this + "OnHitObject" + hit.collider.gameObject.name);
		//IDamageable _scp = _col.GetComponent<IDamageable>();
		//if (_scp != null) {
		//	_scp.TakeDamage(damage);
		//}

		//Sound, Particle
		PoolMaster _p = PoolManager.ins.Instantiate("EffectCollected", _hitPoint, Quaternion.identity).GetComponent<PoolMaster>();
		_p.Play ();

		SoundManager.ins.Play ("ItemEat", false);

		Destroy ();
	}

	public void Destroy(){
		gameObject.SetActive (false);
	}
}
