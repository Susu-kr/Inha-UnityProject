using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour
{
    [Range(0, 1)]
    public float posWeight = 1;

    [Range(0, 1)]
    public float rotWeight = 1;

    public Transform rightHandFollowObj;
    protected Animator anim;

    private int selectWeight = 1;

    [Range(0, 359)]
    public float xRot = 0.0f;

    [Range(0, 359)]
    public float yRot = 0.0f;

    [Range(0, 359)]
    public float zRot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // #. 포지션
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectWeight = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectWeight = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectWeight = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectWeight = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectWeight = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectWeight = 6;
        }
        // #. 회전
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if(anim && rightHandFollowObj)
        {
            switch(selectWeight)
            {
                case 1: SetPositionWeight(); break;
                case 2: SetRotationWeight(); break;
                case 3: SetEachWeight(); break;
                case 4: SetRotationAngle(); break;
                case 5: SetLegWeight(); break;
                case 6: SetLookAtObj(); break;
                default: break;
            }
        }
    }

    private void SetPositionWeight()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);

        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandFollowObj.position);
        Quaternion handRot = Quaternion.LookRotation(rightHandFollowObj.position - transform.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, handRot);
    }

    private void SetRotationWeight()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandFollowObj.position);


    }

    private void SetEachWeight()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandFollowObj.position);
        Quaternion handRot = Quaternion.LookRotation(rightHandFollowObj.position - transform.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, handRot);
    }

    private void SetRotationAngle()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandFollowObj.position);
        Quaternion handRot = Quaternion.Euler(xRot, yRot, zRot);
        anim.SetIKRotation(AvatarIKGoal.RightHand, handRot);
    }

    private void SetLegWeight()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, posWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rotWeight);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightHandFollowObj.position);
        Quaternion handRot = Quaternion.LookRotation(rightHandFollowObj.position - transform.position);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, handRot);
    }

    // 대상 쳐다보기
    private void SetLookAtObj()
    {
        anim.SetLookAtWeight(1);
        anim.SetLookAtPosition(rightHandFollowObj.position);
    }
}
