using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;
    float viewWidth;

    private void Awake()
    {
        viewWidth = 950;
    }

    void Update()
    {
        Move();
        Scrolling();
    }

    void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.left * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void Scrolling()
    {
        // Scrolling : ��� �̾����� �ϱ�
        if (sprites[endIndex].position.x < viewWidth * (-1))
        {
            // #. Sprite Reuse
            Vector3 backSpritesPos = sprites[startIndex].localPosition;
            Vector3 frontSpritesPos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritesPos + Vector3.right * viewWidth;

            // #. Cursor Index Change
            startIndex += 1;
            if (startIndex == sprites.Length) startIndex = 0;
            endIndex += 1;
            if (endIndex == sprites.Length) endIndex = 0;
        }
        // Parallax : �Ÿ��� ���� ����� �ӵ��� Ȱ���� ��� (���ٰ�)
    }
}
