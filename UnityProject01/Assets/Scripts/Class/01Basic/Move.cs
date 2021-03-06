using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0.0f, 3.0f, 0.0f);
        //this.transform.Translate(new Vector3(0.0f, 3.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        // Key 처리
        Move_3();
    }

    void Move_1()
    {
        float moveDelta = this.moveSpeed * Time.deltaTime;
        Vector3 pos = this.transform.position;
        pos.z += moveDelta;
        this.transform.position = pos;
    }

    void Move_2()
    {
        if (Input.GetKey(KeyCode.W))
        {
            float moveDelta = this.moveSpeed * Time.deltaTime;
            this.transform.Translate(Vector3.forward * moveDelta);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float moveDelta = this.moveSpeed * Time.deltaTime;
            this.transform.Translate(Vector3.back * moveDelta);
        }
    }

    void Move_3()
    {
        float z = Input.GetAxis("Vertical"); // w : +,  s : - (1 ~ -1)
        z = z * moveSpeed * Time.deltaTime; // 이동량
        gameObject.transform.Translate(Vector3.forward * z); // 앞뒤로 움직임

    }
}
