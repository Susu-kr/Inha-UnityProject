using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanimControl : MonoBehaviour
{
    public float runSpeed = 6.0f;
    public float rotSpeed = 360.0f;
    public bool isAttack = false;

    CharacterController characterController;
    Vector3 direction;

    Animator anim;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", characterController.velocity.magnitude);
        CharacterControl_Slerp();
        Input_Animation();
    }

    void Input_Animation()
    {
        if(Input.GetButton("Fire1") && !isAttack)
        {
            //anim.SetTrigger("Attack");
            isAttack = true;
            anim.SetBool("Attack", isAttack);
            StartCoroutine("Attack_Routine");
        }
    }

    IEnumerator Attack_Routine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.0f);
            if(isAttack && anim.GetCurrentAnimatorStateInfo(1).IsName("Upperbody.HandsUp"))
            {
                if(anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1.0f)
                {
                    isAttack = false;
                    anim.SetBool("Attack", isAttack);
                    break;
                }
            }

            //if (isAttack && anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
            //{
            //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //    {
            //        isAttack = false;
            //        anim.SetBool("Attack", isAttack);
            //        break;
            //    }
            //}
        }
    }

    void CharacterControl_Slerp()
    {
        direction = new Vector3(
            Input.GetAxis("Horizontal")
            , 0
            , Input.GetAxis("Vertical"));

        if(direction.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(
                transform.forward
                , direction
                , rotSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + forward);
        }
        else
        {

        }

        characterController.Move(direction * runSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
    }
}
