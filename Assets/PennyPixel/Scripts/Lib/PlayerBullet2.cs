using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolManager5;

public class PlayerBullet2 : MonoBehaviour
{
    public LayerMask mask;
    public float speed = 30f;
	Transform trans;
	Vector3 curPoint, nextPoint;
	RaycastHit2D hit;
	int dir;
	float moveDistance, runTime, damage = 1f;
	public float RUN_TIME = 2f;


    void Start()
    {
        trans = transform;
    }

	public void InitFirst(int _dir, float _damage){
		dir 	= _dir;
		damage 	= _damage;
		runTime = Time.time + RUN_TIME;
	}

    void Update()
    {
		Vector3 _dirV = dir == 0 ? -trans.right : trans.right;
        moveDistance    = speed * Time.deltaTime;
		curPoint 		= trans.position;
		nextPoint       = trans.position + _dirV * moveDistance;
		trans.Translate(_dirV * moveDistance, Space.World);

        #if UNITY_EDITOR
		Debug.DrawLine(curPoint, nextPoint, Color.green);
		#endif
		hit = Physics2D.Linecast(curPoint, nextPoint, mask);
		if (hit)
        {
            IDamageable _scp = hit.collider.GetComponent<IDamageable>();
            if (_scp != null)
            {
                _scp.TakeDamage(damage);
            }

            //Sound, Particle
            ParticleSystem _p = PoolManager.ins.Instantiate("EffectHit", hit.point, Quaternion.identity).GetComponent<ParticleSystem>();
            _p.Stop();
            _p.Play();

            //SoundManager.ins.Play ("ShellExplosion", -1);
            PoolReturn();
        }

		if (Time.time > runTime)
        {
            PoolReturn();
        }                
    }

    void PoolReturn()
    {
        gameObject.SetActive(false);
    }
}
