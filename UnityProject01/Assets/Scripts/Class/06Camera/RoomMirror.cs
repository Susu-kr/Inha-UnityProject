using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMirror : MonoBehaviour
{
    private Camera m_cam;
    // Start is called before the first frame update
    void Start()
    {
        m_cam = GetComponent<Camera>();
    }

    private void OnPreCull()
    {
        m_cam.ResetProjectionMatrix();
        // x 축으로 반전
        m_cam.projectionMatrix = m_cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }
    private void OnPreRender()
    {
        // Graphic Library
        GL.invertCulling = true;
    }
    private void OnPostRender()
    {
        GL.invertCulling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
