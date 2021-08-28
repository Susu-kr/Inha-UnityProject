using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float JumpPower = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!FlappyBird.Instance.isDeath)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, JumpPower, 0);
                transform.rotation = Quaternion.Euler(0, 0, 45f);
            }
            transform.Rotate(0, 0, -0.13f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            FlappyBird.Instance.isDeath = true;
            FlappyBird.Instance.ChangeScene("04-03 Flappy Bird End");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FlappyBird.Instance.score++;
        Debug.Log("Score : " + FlappyBird.Instance.score);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), "Score : " + FlappyBird.Instance.score);
    }
}
