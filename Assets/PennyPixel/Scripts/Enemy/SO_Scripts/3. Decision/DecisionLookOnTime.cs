using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO{
	[CreateAssetMenu(menuName="Pluggable/Enemy/3. Decision/LookOnTime")]
	public class DecisionLookOnTime : Decision {
		
		public override bool Decide(EnemyController _c){
			return _c.CheckLockOnTime (_c.chaseLookOnTime);
		}
	}
}