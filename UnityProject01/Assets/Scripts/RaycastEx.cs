using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEx : MonoBehaviour
{
    [Range(0, 50)] // attribute
    public float distance = 10.0f;
    private RaycastHit rayHit;
    private RaycastHit[] rayHits;
    private Ray ray;

    private Transform otherTrans = null;

    private void Awake()
    {
        otherTrans = GameObject.Find("Other").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(this.transform.position, this.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        //Ray_1();
        //Ray_2();
        //Ray_3();
        Ray_FindObj();
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        //Gizmos.DrawLine(ray.origin, ray.direction * distance);
        Gizmos.color = new Color32(255, 242, 0, 255); // Color.yellow;
        Gizmos.DrawWireSphere(ray.origin, 0.5f);

        DrawGizmos();
    }

    private void DrawGizmos()
    {
        if(rayHits != null)
        {
            for (int index = 0; index < this.rayHits.Length; index++)
            {
                if (this.rayHits[index].collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(this.rayHits[index].point, 0.1f);


                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(transform.position,
                        transform.position + transform.forward * rayHits[index].distance);


                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(rayHits[index].point, rayHits[index].point + rayHits[index].normal);

                    // 반사방향
                    Gizmos.color = new Color(1.0f, 0.0f, 1.0f);
                    Vector3 reflect = Vector3.Reflect(transform.forward, rayHits[index].normal);
                    Gizmos.DrawLine(rayHits[index].point, rayHits[index].point + reflect);
                }
            }
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        }
    }

    void Ray_1()
    {
        if(Physics.Raycast(ray.origin, ray.direction, out rayHit, distance))
        {
            Debug.Log(rayHit.collider.gameObject.name);
        }
    }

    void Ray_2()
    {
        rayHits = Physics.RaycastAll(ray, distance);

        for (int index = 0; index < rayHits.Length; index++)
        {
            Debug.Log(rayHits[index].collider.gameObject.name + "hit!!");
        }
    }

    void Ray_3()
    {
        rayHits = Physics.RaycastAll(ray, distance);
        for(int index = 0; index < rayHits.Length; index++)
        {
            if (rayHits[index].collider.gameObject.tag == "Box")
            {
                Debug.Log(rayHits[index].collider.gameObject.name + " - hit!! - find of Tag");
            }
            if (rayHits[index].collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log(rayHits[index].collider.gameObject.name + " - hit!! - find of Layer");
            }
        }
    }

    void Ray_FindObj()
    {
        Vector3 dir = otherTrans.position - this.transform.position;
        dir.Normalize();

        float dist = Vector3.Distance(otherTrans.position, this.transform.position);
        Debug.DrawRay(ray.origin, dir * dist, Color.red);

        rayHits = Physics.SphereCastAll(ray, 1.0f, distance);

        for (int index = 0; index < rayHits.Length; index++)
        {
           // if(rayHits[index].collider != null)
            {
                // 삭제
                //Destroy(rayHits[index].collider.gameObject);
                rayHits[index].collider.gameObject.SetActive(false);
            }
        }
    }
}
