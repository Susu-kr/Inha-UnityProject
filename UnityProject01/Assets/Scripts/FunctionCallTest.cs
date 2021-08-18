using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCallTest : MonoBehaviour
{
    private void Awake()
    {
        // console â�� ���
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
        // ����� ���ɿ� ���� �ӵ��� �޶��� -> DeltaTime ����ϴ� ����
        // Timer or Key �Է�
        print("Update Called ~ !");
    }

    private void LateUpdate()
    {
        // ��� Object���� Update�� ����� ��
        // ī�޶� ���� ó����
        print("LateUpdate Called ~ !");

    }

    private void FixedUpdate()
    {
        // Default = 0.02��
        // ������ �浹���� -> ��Ģ���� �ֱ⸦ ���� ó��
        print("FixedUpdate Called ~ !");

    }

    private void OnDisable()
    {
        print("OnDisable Called ~ !");
    }

    private void OnDestroy()
    {
        // ������� ������ ���ϴ� �ൿ��
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
        // ����� �׽�Ʈ �ɼ� ��ġ�� �ٲ��ְ��� �Ҷ�
    }
}
