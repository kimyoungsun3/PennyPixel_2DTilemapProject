using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : FSM<GAME_STATE> {
	#region Variable Zone
	private static GameManager ins_;
	public static GameManager ins{
		get{	return ins_;	}
		set{ 	ins_ = value; 	}
	}
	//public Ui_Result uiResult;
	//WaveSpawner spawner;
	#endregion

	#region 게임 Instance 클래스...
	PlayerPlatformerController player;
	EnemySpawner enemySpawner;
	//public bool bPause;

	[Header("개발링크")]
	public Ui_SceneInfo 	uiSceneInfo;
	public Ui_Result 		uiResult;
	public VirtualJoystick	uiJoystick;
	public Ui_RightButton	uiRightButton;
	//public Ui_Crosshair 	uiCrosshair;
	#endregion

	#region 게임 Variable
	float waitTime;
	public float GAME_END_WAITTIME = 3f;
	#endregion


	void Awake(){
		ins = this;
		SetEnvironment ();		
	}

	void SetEnvironment(){
		//에디터 모드에서 실행.
		#if UNITY_EDITOR
		Application.runInBackground 	= true;//editor mode back ground is run..
		//Cursor.visible 				= false;
		#endif

		//아직미검증....
		//Debug.Log ("### -> frame -> 발열해결 ->속제한해제된상태...");
		//V sync makes the frame display update wait for the screen refresh update, 
		//in other words it ties your frame rate to your monitors refresh rate(60htz, 100htz etc).
		//If you set it to 0 then it ignores your monitor refresh and outputs the frames as fast as possible.
		//If you set it to 1 then it will output one frame update for every screen update.
		//If you set it to 2 it will output one frame update every second screen update.
		//This is often used for trying to stop screen tearing issues by keeping the two in sync.
		//-1   : 끊어짐 발생
		//0    : 끊어짐 발생
		//1    : 정상...
		//2    : 끊어짐 발생
		//3    : 끊어짐이 심해짐...
		//4    : 더끊어짐이 심해짐...
		//QualitySettings.vSyncCount 	= 1;
		Application.targetFrameRate = 55;	

		//55 -> 발열... 
	}

	void Start () {
		AddState(GAME_STATE.Ready, 	pInReady, 	ModifyReady, 	null);
		AddState(GAME_STATE.Gaming, pInGaming, 	ModifyGaming, 	null);
		AddState (GAME_STATE.GameEndWait, pInGameEndWait, ModifyGameEndWait, null);
		AddState(GAME_STATE.Result, pInResult, 	ModifyResult, 	null);

		MoveState(GAME_STATE.Ready);
	}

	//-----------------------------------------------------------
	//--- Ready ---
	//-----------------------------------------------------------
	public void pInReady(){
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerPlatformerController>();
		}
		player.DisableController();
		uiSceneInfo.SetActive2 (true);
		uiSceneInfo.SetMessage ("Penny Adventure");

		SoundManager.ins.Init ();
		SoundManager.ins.Play ("BGM", true);
	}

	void ModifyReady(){
		if (Input.anyKeyDown) {
			MoveState(GAME_STATE.Gaming);
			return;
		}
	}

	//-----------------------------------------------------------
	//--- Gaming ---
	//-----------------------------------------------------------
	//public void Restart(){
	//	MoveState(GAME_STATE.Gaming);
	//}

	public void pInGaming(){
		uiSceneInfo.SetActive2 (false);
		uiResult.SetActive2 (false);

		//Sound plays Main Theme.
		//SoundManager.ins.Play ("Main theme", true);

		//user info initiaize
		player.InitFirst ();
		player.EnableController();

		//Spawner Setting...
		//EnemySpawner.ins.EnableControl (0, player.transform);
	}

	void ModifyGaming(){
		//Gaming state
		if (player.bDeath) {
			MoveState(GAME_STATE.GameEndWait);
			return;
		}
	}

	//-----------------------------------------------------------
	// Gaming -> GameEndWait(5 secondes wait) -> Result
	//-----------------------------------------------------------
	void pInGameEndWait(){
		waitTime = Time.time + GAME_END_WAITTIME;

		//Player disable
		player.DisableController();
		//SoundManager.ins.Play ("Menu theme", true);

		//Spawner disable.
		//EnemySpawner.ins.DisableControl();
	}

	void ModifyGameEndWait(){
		if (Time.time > waitTime) {
			MoveState(GAME_STATE.Result);
			return;
		}
	}

	//-----------------------------------------------------------
	//--- Result ---
	//-----------------------------------------------------------
	void pInResult(){
		//Spawn Wave -> pInGame...
		uiResult.SetActive2 (true);
		uiResult.SetMessage ("Game Over");
	}

	void ModifyResult(){
		if (Input.anyKeyDown) {
			MoveState(GAME_STATE.Gaming);
			return;
		}
	}

}
