using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public int dmg;
    public bool isRotate; // ������ ���ư��� �Ѿ˷� ����

    void Update()
    {
        if (isRotate)
            transform.Rotate(Vector3.forward * 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border Bullet")
        {
           gameObject.SetActive(false);
        }
      
    }
}
