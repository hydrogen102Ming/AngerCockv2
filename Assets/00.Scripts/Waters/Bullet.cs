
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 0.1f,radius=0.2f,damage=1; // �ӵ�,�Ѿ� ������, ���ط�
    public LayerMask layerMask;
    [SerializeField]
    private float _maxTime; //�ִ� LifeTime (���� �� �� �ð��� ������ �Ѿ� �����)
    private float _time;
    internal RaycastHit _hit;
    public BulletTypeMing bulletType;

    internal void OnEnable()
    {
        _time = 0; // �Ѿ� LifeTime �ʱ�ȭ
    }

    public void FixedUpdate()
    {
        if(Physics.SphereCast(transform.position,radius,transform.forward,out _hit,speed,layerMask))
        {
            //if(_hit.transform.TryGetComponent<Hp>(out var hp))
            //{
            //    hp.Damage(damage); // ��� �����ϱ�
            //}
            Collision();
            BulletPool.Instance.CollectBullets(gameObject,bulletType); // �Ѿ� Ǯ�� ���ֱ� ����
        }
            transform.position += transform.forward * speed; //�Ѿ� �̵�(���̷� �� ������ ������ �̵�)
            _time += Time.fixedDeltaTime;

        if(_time >= _maxTime)
        {
            BulletPool.Instance.CollectBullets(gameObject, bulletType); // �Ѿ� Ǯ�� ��
        }
    }
    public virtual void Collision()
    {

    }
}
