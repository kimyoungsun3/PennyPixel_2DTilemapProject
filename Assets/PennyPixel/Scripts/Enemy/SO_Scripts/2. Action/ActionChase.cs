using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SO{
	[CreateAssetMenu(menuName="Pluggable/Enemy/2. Action/Chase")]
	public class ActionChase : Action {

		//Quaternion quat = Quaternion.identity;
		public override void Act(EnemyController _c){
			//move
			//Debug.Log ("Action -> Chase" + _c.target);
			float _distance = Vector3.Distance (_c.target.position, _c.trans.position);

			if (_distance > _c.chaseDistance) {				
				_c.trans.position = Vector3.MoveTowards (
					_c.trans.position, 
					_c.target.position, 
					_c.speedChase * Time.deltaTime
				);
			}

			#if UNITY_EDITOR
			//Debug.Log (_c.spriteRenderer.flipX + ":" + _dir);
			Vector3 _dir = _c.spriteRenderer.flipX ? Vector3.left : Vector3.right;
			//Vector3 _nextPoint = _c.trans.position + _dir * _c.seeDistance;
			Debug.DrawRay (_c.trans.position, _dir * _c.chaseDistance, _c.stateCurrent.gizmosColor);
			#endif

			//rotation.
			//Quaternion _q = Quaternion.LookRotation (_c.target.position - _c.trans.position);
			//Debug.Log (_q.eulerAngles.y +":"+  _c.trans.rotation.eulerAngles.y);
			//if (!Mathf.Approximately(_q.eulerAngles.y, _c.trans.rotation.eulerAngles.y)) {
			//	//Debug.Log (" > " + _q.eulerAngles.y +":"+  _c.trans.rotation.eulerAngles.y);
			//	_c.trans.rotation = Quaternion.Lerp (_c.trans.rotation, _q, .2f);
			//}


			//Quaternion _q = Quaternion.LookRotation (_c.target.position - _c.trans.position);
			//_c.trans.rotation = Quaternion.RotateTowards(_c.trans.rotation, _q, _c.stateInfo.speedTurn * Time.deltaTime );

			//rotation. > 정확하지 않는다....
			// (_c.trans.rotation != quat) {
			//	quat = Quaternion.LookRotation (direction);
			//	_c.trans.rotation = Quaternion.RotateTowards (_c.trans.rotation, quat, .2f);
			//}
		}
	}
}
