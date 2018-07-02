using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolManager5;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
	private Animator animator;

	//----------------------------
	public VirtualJoystick btnJoystick;
	public Ui_RightButton uiBtnRight;
	Vector3 move;
	bool bJump, bAttack, bSkill1, bSkill2;
	float waitAttackTime, waitSkill1Time, waitSkill2Time;
	public float WAIT_ATTACK_TIME = 1f;
	public float WAIT_SKILL1_TIME = 2f;
	public float WAIT_SKILL2_TIME = 2f;
	public Transform[] spawnPoint;
	int spawnDir = 0;
	public bool bDeath;


    // Use this for initialization
    void Awake () 
    {
        spriteRenderer 	= GetComponent<SpriteRenderer> (); 
        animator 		= GetComponent<Animator> ();
    }

	//---------------------------------
	public void InitFirst(){
		bDeath = false;
	}

	public void EnableController(){
		//Debug.Log ("EnableController:" + btnJoystick + ":" + uiBtnRight);
		btnJoystick.SetActive2 (true);
		uiBtnRight.SetActive2 (true);
	}

	public void DisableController(){
		//Debug.Log ("DisableController:" + btnJoystick + ":" + uiBtnRight);
		btnJoystick.SetActive2 (false);
		uiBtnRight.SetActive2 (false);
	}

	//-----------------------------------
    protected override void ComputeVelocity()
    {
		//--------------------------
		// Left - Right move > get
		//--------------------------
		if (btnJoystick.InputVector != Vector3.zero) {
			//Joystick 
			move = btnJoystick.InputVector;
		} else {
			//Keyboard (move)
			move.Set (Input.GetAxis ("Horizontal"), 0, 0);
		}


		//--------------------------
		// Jump, 
		//--------------------------
		if ((uiBtnRight.GetJumpDown() || Input.GetButtonDown("Jump")) && grounded) {
            velocity.y = jumpTakeOffSpeed;
		} else if (uiBtnRight.GetJumpUp()|| Input.GetButtonUp("Jump")) {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
           }
        }

		//-------------------------------------------
		//Attack, Attack, Skill1,2
		//spawnDir [L 0] [R 1]
		//-------------------------------------------
		if ((uiBtnRight.GetAttackDown () || Input.GetKeyDown(KeyCode.P)) && Time.time > waitAttackTime ) {
			waitAttackTime = Time.time + WAIT_ATTACK_TIME;
			PlayerBullet2 _scp = PoolManager.ins.Instantiate ("PlayerBullet2", spawnPoint[spawnDir].position, spawnPoint[spawnDir].rotation).GetComponent<PlayerBullet2>();
			_scp.InitFirst (spawnDir, 1f);
		}

		if ((uiBtnRight.GetSkill1Down () || Input.GetKeyDown(KeyCode.I)) && Time.time > waitSkill1Time) {
			waitSkill1Time = Time.time + WAIT_SKILL1_TIME;
			PlayerBullet2 _scp = PoolManager.ins.Instantiate ("PlayerBulletSkill1", spawnPoint[spawnDir].position, spawnPoint[spawnDir].rotation).GetComponent<PlayerBullet2>();
			_scp.InitFirst (spawnDir, 1f);
		}

		if ((uiBtnRight.GetSkill2Down () || Input.GetKeyDown(KeyCode.I)) && Time.time > waitSkill2Time) {
			waitSkill2Time = Time.time + WAIT_SKILL2_TIME;
			PlayerBullet2 _scp = PoolManager.ins.Instantiate ("PlayerBulletSkill2", spawnPoint[spawnDir].position, spawnPoint[spawnDir].rotation).GetComponent<PlayerBullet2>();
			_scp.InitFirst (spawnDir, 1f);
		}

		//-------------------------------------------
        if(move.x > 0.01f)
        {
			spawnDir = 1;
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
				spawnDir = 1; 
            }
        } 
        else if (move.x < -0.01f)
		{
			spawnDir = 0;
            if(spriteRenderer.flipX == false)
            {
				spriteRenderer.flipX = true;
				spawnDir = 0; 
            }
        }

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}