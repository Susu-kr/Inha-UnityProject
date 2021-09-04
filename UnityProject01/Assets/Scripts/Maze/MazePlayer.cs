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
            Cursor.visible = false; // ���콺 Ŀ���� ������ �ʰ� 
            Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��ġ ����          
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

        // �̵� �ӵ� ���� (������ �̵��Ҷ� 5, ������ 2)
        movement3D.MoveSpeed = z > 0 ? 5.0f : 2.0f;
        // �̵� �Լ� ȣ�� (ī�޶� ���� ������ �������� ����Ű�� ���� �̵�)
        movement3D.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));
        // ī�޶� ȸ�� �� ĳ���� ȸ��
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
