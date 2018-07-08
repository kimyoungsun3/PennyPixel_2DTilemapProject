using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SO{
	[CreateAssetMenu(menuName="Pluggable/Enemy/1. State")]
	public class State : ScriptableObject {
		public Action[] actions;
		public Transition[] transitions;
		public Color gizmosColor = Color.grey;

		public void UpdateState(EnemyController _c){
			for (int i = 0; i < actions.Length; i++) {
				actions [i].Act (_c);
			}

			bool _bDecide;
			for (int i = 0; i < transitions.Length; i++) {
				_bDecide = transitions [i].decide.Decide (_c);
				//Debug.Log (i + ":" + bDecide);
				if (_bDecide) {
					_c.SetState(transitions [i].stateTrue);
				} else {
					_c.SetState(transitions [i].stateFalse);
				}
			}
		}
	}

	[System.Serializable]
	public class Transition {
		public Decision decide;
		public State stateTrue, stateFalse;
	}
}