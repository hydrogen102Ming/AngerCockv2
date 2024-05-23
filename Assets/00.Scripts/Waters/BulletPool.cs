
using System.Collections.Generic;
using UnityEngine;

public enum BulletTypeMing
{
    Ball,
    Bullet,
    Rocket,
    Granade,
    Beam,
    waterGround,
    waterCock,
    waterWeb
}

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject[] bullet;               //�Ѿ�������
    private GameObject[] _currentbullet = new GameObject[5];
    //private Stack<GameObject> _stack = new Stack<GameObject>();
    private Stack<GameObject>[] _stack  = {new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>() }; // ���������� �Ѿ� Ǯ���� �� ����
    private void Awake()
    {
        Instance = this;
    }

    public void CollectBullets(GameObject bullet,BulletTypeMing bulletType)
    {
        _stack[(int)bulletType].Push(bullet);                //�Ѿ� ȸ��
        _stack[(int)bulletType].Peek().SetActive(false);
    }
    public void GiveBullets(Transform targetpos,BulletTypeMing bulletType)
    {
        
        if (!_stack[(int)bulletType].TryPop(out _currentbullet[(int)bulletType]))
        {
            _currentbullet[(int)bulletType] = Instantiate(bullet[(int)bulletType]);       //�Ѿ��� ������ ����
        }

        _currentbullet[(int)bulletType].transform.SetPositionAndRotation(targetpos.position, targetpos.rotation);    //�Ѿ� �����ֱ�
        _currentbullet[(int)bulletType].SetActive(true);
    }
}
