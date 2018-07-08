using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO{
	[CreateAssetMenu(menuName="Pluggable/Enemy/2. Action/Attack")]
	public class ActionAttack : Action {
		
		public override void Act(EnemyController _c){
			Vector3 _dir = _c.spriteRenderer.flipX ? Vector3.left : Vector3.right;

			#if UNITY_EDITOR
			Debug.DrawRay (_c.trans.position, _dir * _c.attackDistance, _c.stateCurrent.gizmosColor);
			#endif

			RaycastHit2D _hit = Physics2D.Raycast(_c.trans.position, _dir, _c.attackDistance, _c.mask);
			if(_hit)
			{
				_c.Shoot ();
			}

			//if (Physics.Raycast (_c.trans.position, _c.trans.forward, out hit, _c.attackDistance, _c.mask)) {
				
			//}


		}
	}
}