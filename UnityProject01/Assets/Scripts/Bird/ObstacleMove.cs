using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = -5.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (FlappyBird.Instance.score % 10 == 0 && FlappyBird.Instance.score != 0)

        {
            speed -= 0.1f;
        }
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
