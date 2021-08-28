using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayer : MonoBehaviour
{
    public Transform cam;
    public Transform Player;
    CharacterController pControl;

    Animation anim;
    public float runSpeed = 6.0f;
    public float rotSpeed = 360.0f;

    public GameObject objSword = null;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
        anim.wrapMode = WrapMode.Loop;
        pControl = gameObject.GetComponent<CharacterController>();

        objSword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine("AttackToIdle");
        }
        if(anim.IsPlaying("attack") == false)
        {
            Move();
        }
        if(DefenseGameManager.Instance.isDeath == true)
            DefenseGameManager.Instance.ChangeScene("Defense_End");

    }

    private void Move()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal")
            , 0, Input.GetAxis("Vertical"));
        velocity *= runSpeed;
        if (velocity.magnitude > 0.5)
        {
            anim.CrossFade("run", 0.3f);
            // 캐릭터 회전 자연스럽게
            Vector3 forward = Vector3.Slerp(Player.transform.forward,
                velocity, rotSpeed * Time.deltaTime / Vector3.Angle(Player.transform.forward, velocity));

            Player.transform.LookAt(Player.transform.position + forward);
        }
        else
        {
            anim.CrossFade("idle", 0.3f);
        }
        

        pControl.SimpleMove(velocity);
    }

    IEnumerator AttackToIdle()
    {
        if (anim.IsPlaying("attack") == true)
            yield break;
        // 1. attack 실행

        objSword.SetActive(true);
        anim.wrapMode = WrapMode.Once;
        anim.CrossFade("attack", 0.3f);

        // 2. delayTime 만큼 대기
        float delayTime = anim.GetClip("attack").length - 0.3f;
        yield return new WaitForSeconds(delayTime);

        // 3. 대기시간 이후 idle 실행

        objSword.SetActive(false);
        anim.wrapMode = WrapMode.Loop;
        anim.CrossFade("idle", 0.3f);
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), "Score : " + DefenseGameManager.Instance.score);
    }
}
