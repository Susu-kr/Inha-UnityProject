using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    Rigidbody2D rig2D;

    private void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rig2D.velocity = Vector2.left * 50.0f;
    }
}
