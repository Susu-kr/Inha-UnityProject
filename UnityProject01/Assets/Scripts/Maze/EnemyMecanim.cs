using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMecanim : MonoBehaviour
{
    public GameObject Target; // 쫓을 목표
    public MazeManager mazeManager;
    public GameData gameData;
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
        transform.position = new Vector3(0,0,0);
        Invoke("Chacing", 5f);
    }

    public void Chacing()
    {
        if (Target)
        {
            agent.destination = Target.transform.position;

            // 경로 나타내는 법
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(Target.transform.position, path);

        }
        // agent.SetDestination = Target.transform.position;




        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnDrawGizmos()
    {
        if(Target)
        {
            float fRed = 0.0f;
            for (int i = 0; i < agent.path.corners.Length; i++)
            {
                Gizmos.color = new Color(1 - fRed, fRed, fRed);
                Gizmos.DrawSphere(agent.path.corners[i], 0.3f);
                fRed += 0.2f;
            }
        }
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
