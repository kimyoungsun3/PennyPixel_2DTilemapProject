using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor {

	EnemyController controller;
	Transform trans;
	void OnEnable(){
		controller = target as EnemyController;
		trans = controller.transform;
	}

	void OnSceneGUI(){
		Vector3[] _lp = controller.wayLocal;
		Vector3 _wp, _wp2 = Vector3.zero;
		Quaternion _rot = Tools.pivotRotation == PivotRotation.Local ? trans.rotation : Quaternion.identity;

		int _len = _lp.Length;
		if(Application.isPlaying){
			for(int i = 1; i < _len; i++){
				Handles.DrawLine (controller.wayWorld[i - 1], controller.wayWorld[i]);
			}
			return;
		}

		Handles.color = Color.white;
		EditorGUI.BeginChangeCheck ();
		for (int i = 0; i < _len; i++) {
			_wp = trans.TransformPoint (_lp [i]);
			_wp = Handles.DoPositionHandle (_wp, _rot);
			_lp [i] = trans.InverseTransformPoint (_wp);

			if (i >= 1) {
				Handles.DrawLine (_wp2, _wp);
			}
			_wp2 = _wp;
		}

		if (EditorGUI.EndChangeCheck ()) {
			Undo.RecordObject (controller, "MovePoint");
			EditorUtility.SetDirty (controller);
		}
	}
}
