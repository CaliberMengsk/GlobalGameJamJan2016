using UnityEngine;
using System.Collections;

public class WanderingEnemy : MonoBehaviour {

    public float visionRange = 10;
    public float attackRange = 2;
    public float delayAttack = 1.5f;

    Vector3 hitPoint;
    private NavMeshAgent agent;
    NavMeshPath path;
    Animator meshAnim;
    bool isDead = false;

    // Use this for initialization
    void Start()
    {
        meshAnim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        agent.SetPath(path);

    }

    // Update is called once per frame
    void Update() {
        if (!isDead)
        {
            float distance = Vector3.Distance(Globals.player.transform.position, transform.position);
            if (distance < visionRange && distance > attackRange && !meshAnim.GetBool("isAttacking"))
            {
                agent.SetDestination(Globals.player.transform.position);
                meshAnim.SetBool("isRunning", true);
                agent.updatePosition = true;
            }
            else
            {
                agent.SetDestination(transform.position);
                meshAnim.SetBool("isRunning", false);
                agent.updatePosition = true;
            }
            if (distance <= attackRange)
            {
                meshAnim.SetFloat("delayAttack", meshAnim.GetFloat("delayAttack") - Time.deltaTime);
                agent.SetDestination(transform.position);
                agent.updatePosition = false;
                meshAnim.SetBool("isRunning", false);
                meshAnim.SetBool("isAttacking", true);
            }
            else
            {
                meshAnim.SetBool("isAttacking", false);
            }
            isDead = meshAnim.GetBool("isDead");
        }
        else
        {
            agent.updatePosition = false;
            Globals.player.GetComponent<Player>().RemoveTarget(gameObject);
            transform.position += new Vector3(0,.3f* -Time.deltaTime, 0);
            Invoke("DestroySelf", 6);
        }
    }

    public void AttackPlayer()
    {
        float distance = Vector3.Distance(Globals.player.transform.position, transform.position);
        if (distance <= attackRange && !isDead)
        {
            meshAnim.SetFloat("delayAttack", delayAttack);
            Globals.player.GetComponentInChildren<Stats>().TakeDamage(transform.GetComponentInChildren<Stats>().strength);
        }
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
