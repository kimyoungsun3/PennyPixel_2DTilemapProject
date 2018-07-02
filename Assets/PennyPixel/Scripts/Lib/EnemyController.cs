using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public float speed = 4f;
    public float HEALTH_MAX = 3f;
    float health = 3;
    bool bDie = false;
    Transform trans, transPlayer;

    void Awake()
    {
        trans = transform;
    }

    void OnEnable()
    {
        //if(PlayerController.ins != null)
        //
        //    transPlayer = PlayerController.ins.transform;
        //}
        
        health  = HEALTH_MAX;
        bDie    = false;            
    }

	public void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDirection){}
	public void TakeDamage(float _damage) {
        health -= _damage;
        //Debug.Log(health + ":" + bDie);

        if(health <= 0f && !bDie)
        {
            bDie = true;
            gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(transPlayer == null)
        {
            return;
        }

        //Debug.Log(transPlayer + ":" + trans);
        trans.rotation = Util.GetQuaternionFormDir(transPlayer.position - trans.position);
        trans.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
