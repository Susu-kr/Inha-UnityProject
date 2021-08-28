using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        anim.Play("die");

        float delayTime = anim.GetClip("die").length - 0.3f;
        yield return new WaitForSeconds(delayTime);

        Destroy(this.gameObject, 1.0f);
    }
}
