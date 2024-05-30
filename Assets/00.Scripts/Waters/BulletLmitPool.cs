using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LimitBullet
{
    water1
}
public class BulletLmitPool : MonoBehaviour
{
    public static BulletLmitPool Instance;
    public GameObject[] bullet;               //�Ѿ�������
    private GameObject[] _currentbullet = new GameObject[5];
    public int[] count;
    //private Stack<GameObject> _stack = new Stack<GameObject>();
    private Stack<GameObject>[] _stack = { new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>(), new Stack<GameObject>() }; // ���������� �Ѿ� Ǯ���� �� ����
    private Queue<GameObject>[] _queue = { new Queue<GameObject>(), new Queue<GameObject>(), new Queue<GameObject>(), new Queue<GameObject>(), new Queue<GameObject>(), new Queue<GameObject>(), new Queue<GameObject>(), new Queue<GameObject>() }; // ���������� �Ѿ� Ǯ���� �� ����
    private void Awake()
    {
        Instance = this;

        for(int i = 0; i < bullet.Length; i++)
        {
            for(int j = 0; j < count[i]; j++)
            {
                _stack[i].Push(Instantiate(bullet[i]));
                _stack[i].Peek().SetActive(false);
            }
        }
    }



    public void CollectBullets(GameObject bullet, BulletTypeMing bulletType)
    {
        _stack[(int)bulletType].Push(bullet);                //�Ѿ� ȸ��

        _stack[(int)bulletType].Peek().SetActive(false);
        
        _queue[(int)bulletType].Dequeue();
    }
    public void GiveBullets(Transform targetpos, LimitBullet bulletType)
    {

        if (!_stack[(int)bulletType].TryPop(out _currentbullet[(int)bulletType]))
        {
            _currentbullet[(int)bulletType] = _queue[(int)bulletType].Dequeue();       //�Ѿ��� ������ �̾ƿ���
        }
        _queue[(int)bulletType].Enqueue(_currentbullet[(int)bulletType]);
        _currentbullet[(int)bulletType].transform.SetPositionAndRotation(targetpos.position, targetpos.rotation);    //�Ѿ� �����ֱ�
        _currentbullet[(int)bulletType].SetActive(true);
    }
}
