using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO{
	[CreateAssetMenu(menuName="Pluggable/Enemy/3. Decision/ActiveState")]
	public class DecisionActiveState : Decision {

		//RaycastHit hit;
		public override bool Decide(EnemyController _c){
			return (_c.target.gameObject.activeSelf);
		}
	}
}