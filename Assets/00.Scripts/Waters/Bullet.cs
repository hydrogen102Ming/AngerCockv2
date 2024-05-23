
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 0.1f,radius=0.2f,damage=1; // 속도,총알 반지름, 피해량
    public LayerMask layerMask;
    [SerializeField]
    private float _maxTime; //최대 LifeTime (생성 후 이 시간이 지나면 총알 사라짐)
    private float _time;
    internal RaycastHit _hit;
    public BulletTypeMing bulletType;

    internal void OnEnable()
    {
        _time = 0; // 총알 LifeTime 초기화
    }

    public void FixedUpdate()
    {
        if(Physics.SphereCast(transform.position,radius,transform.forward,out _hit,speed,layerMask))
        {
            //if(_hit.transform.TryGetComponent<Hp>(out var hp))
            //{
            //    hp.Damage(damage); // 즉시 공격하기
            //}
            Collision();
            BulletPool.Instance.CollectBullets(gameObject,bulletType); // 총알 풀링 해주기 ㅎㅎ
        }
            transform.position += transform.forward * speed; //총알 이동(레이로 쏜 궤적에 없으면 이동)
            _time += Time.fixedDeltaTime;

        if(_time >= _maxTime)
        {
            BulletPool.Instance.CollectBullets(gameObject, bulletType); // 총알 풀링 ㄱ
        }
    }
    public virtual void Collision()
    {

    }
}
