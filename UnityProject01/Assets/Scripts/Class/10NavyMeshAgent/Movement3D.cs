using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;    // 이동 속도
    private Vector3 moveDirection;  // 이동 방향

    private CharacterController characterController;

    public float MoveSpeed
    {
        // 이동속도는 2 ~ 5 사이의 값만 설정
        set => moveSpeed = Mathf.Clamp(value, 2.0f, 5.0f);
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
    }
}
