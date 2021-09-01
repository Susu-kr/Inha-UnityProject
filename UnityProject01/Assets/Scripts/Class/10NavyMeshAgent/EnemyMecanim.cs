using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMecanim : MonoBehaviour
{
    public GameObject Target; // ÂÑÀ» ¸ñÇ¥
    public MazeManager mazeManager;
    Animator anim;
    NavMeshAgent agent;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Chacing();
    }

    public void TakeDamage()
    {
        anim.SetTrigger("OnHit");
        Invoke("Chacing", 5f);
    }

    public void Chacing()
    {
        if (Target)
            agent.destination = Target.transform.position;
        // agent.SetDestination = Target.transform.position;

        /* 
            NavyMeshPath path = new NavyMeshPath();
            agent.CalculatePath(target.transform.position, path);
         */


        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            collision.gameObject.SetActive(false);
            mazeManager.nextSpawnDelay = true;
            mazeManager.curSpawnDelay = 0;
            score++;
            mazeManager.UpdateEnemyScore(score);
        }
        else if(collision.gameObject.tag == "Player")
        {
            TakeDamage();
        }
    }
}
