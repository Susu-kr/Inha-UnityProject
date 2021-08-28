using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTarget : MonoBehaviour
{
    public int HP;
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Mat_1();
    }

    void Mat_1()
    {
        if (HP < 75)
        {
            child.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (HP < 50)
        {
            child.GetComponent<Renderer>().material.color = Color.magenta;
        }
        if (HP < 25)
        {
            child.GetComponent<Renderer>().material.color = Color.red;
        }
        if(HP <= 0)
        {
            DefenseGameManager.Instance.isDeath = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySword")
        {
            HP--;
        }
    }
}
