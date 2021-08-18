using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCallTest : MonoBehaviour
{
    private void Awake()
    {
        // console 창에 출력
        print("Awake Called ~ !");
    }

    private void OnEnable()
    {
        print("OnEnable Called ~ !");
    }
    // Start is called before the first frame update
    void Start()
    {
        print("Start Called ~ !");
    }

    // Update is called once per frame
    void Update()
    {
        // 기기의 성능에 따라서 속도가 달라짐 -> DeltaTime 사용하는 이유
        // Timer or Key 입력
        print("Update Called ~ !");
    }

    private void LateUpdate()
    {
        // 모든 Object들의 Update가 진행된 후
        // 카메라에 대한 처리들
        print("LateUpdate Called ~ !");

    }

    private void FixedUpdate()
    {
        // Default = 0.02초
        // 물리나 충돌현상 -> 규칙적인 주기를 갖고 처리
        print("FixedUpdate Called ~ !");

    }

    private void OnDisable()
    {
        print("OnDisable Called ~ !");
    }

    private void OnDestroy()
    {
        // 사라지는 시점에 행하는 행동들
        print("OnDestroy Called ~ !");

    }

    private void OnBecameInvisible()
    {
        print("OnBecameInvisible Called ~ !");
    }

    private void OnBecameVisible()
    {
        print("OnBecameVisible Called ~ !");

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(1, 1, 1));
    }

    private void OnGUI()
    {
        // 디버깅 테스트 옵션 수치를 바꿔주고자 할때
    }
}
