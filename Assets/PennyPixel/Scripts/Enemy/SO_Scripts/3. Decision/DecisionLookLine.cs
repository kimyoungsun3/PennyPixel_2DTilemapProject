using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO{
	[CreateAssetMenu(menuName="Pluggable/Enemy/3. Decision/LookLine")]
	public class DecisionLookLine : Decision {

		//RaycastHit hit;
		public override bool Decide(EnemyController _c){
			Vector3 _dir = _c.spriteRenderer.flipX ? Vector3.left : Vector3.right;
			Vector3 _nextPoint = _c.trans.position + _dir * _c.seeDistance;

			#if UNITY_EDITOR
			//Debug.Log (_c.spriteRenderer.flipX + ":" + _dir);
			Debug.DrawLine (_c.trans.position, _nextPoint, Color.green);
			#endif

			//if(Physics2D.Raycast(_c.trans.position, _dir, out hit, _c.seeDistance, _c.mask)){
			//	_c.SetTargeting(hit.transform);
			//	return true;
			//}else{
			//	return false;
			//}
			RaycastHit2D _hit = Physics2D.Linecast(_c.trans.position, _nextPoint, _c.mask);
			if(_hit)
			{
				_c.SetTargeting(_hit.transform);
				return true;
			}else{
				return false;
			}
		}
	}
}