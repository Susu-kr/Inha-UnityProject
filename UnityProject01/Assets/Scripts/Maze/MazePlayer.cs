using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    private Movement3D movement3D;


    Animator anim;
    public int score = 0;
    public MazeManager mazeManager;

    private void Awake()
    {
        /*
            Cursor.visible = false; // 마우스 커서를 보이지 않게 
            Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 위치 고정          
         */
        movement3D = GetComponent<Movement3D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", z);

        // 이동 속도 설정 (앞으로 이동할때 5, 나머지 2)
        movement3D.MoveSpeed = z > 0 ? 5.0f : 2.0f;
        // 이동 함수 호출 (카메라가 보는 방향을 기준으로 방향키에 따라 이동)
        movement3D.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));
        // 카메라 회전 시 캐릭터 회전
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Item")
        {
            hit.gameObject.SetActive(false);
            mazeManager.nextSpawnDelay = true;
            mazeManager.curSpawnDelay = 0;
            score++;
            mazeManager.UpdatePlayerScore(score);
        }
    }
}
