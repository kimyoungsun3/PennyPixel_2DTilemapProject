using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO{
	[CreateAssetMenu(menuName="Pluggable/Enemy/2. Action/Patrol")]
	public class ActionPatrol : Action {
		public override void Act(EnemyController _c){

			//move
			if (_c.trans.position == _c.wayWorld [_c.wayIndex]) {
				_c.wayIndex = (_c.wayIndex + 1) % _c.wayWorld.Length;
			}

			//Debug.Log (_c.moveSpeed + ":" + _c.wayIndex);
			_c.trans.position = Vector2.MoveTowards (_c.trans.position, 
				_c.wayWorld [_c.wayIndex], 
				_c.moveSpeed * Time.deltaTime);

			//rotation.
			//Vector3 _dir = _c.wayWorld [_c.wayIndex] - _c.trans.position;
			//if (_dir != Vector3.zero) {
			//	Quaternion _q = Quaternion.LookRotation (_dir);
			//	_c.trans.rotation = Quaternion.Lerp (_c.trans.rotation, _q, .2f);
			//}
			//rotation X, Sprite Change
			//_c.SetDirAndSprite(_c.wayWorld [_c.wayIndex] - _c.trans.position);

			//
			//Debug.DrawRay (_c.trans.position, _c.trans.forward * _c.stateInfo.seeDistance, Color.green);
		}
	}
}