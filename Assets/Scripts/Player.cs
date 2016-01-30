using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Animator meshAnimator;
    public LayerMask ignoreMask;
    public GameObject targetIndicator;
    public float attackRange = 2.5f;
    public float delayAttack = 1;
    GameObject target;

    Vector3 hitPoint;
    private NavMeshAgent agent;
    NavMeshPath path;


    Vector3 startPos;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        hitPoint = transform.position;
        agent.SetPath(path);
    }
	
	// Update is called once per frame
	void Update () {
        if (!meshAnimator.GetBool("isDead"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (Vector3.Distance(Input.mousePosition, startPos) <= 30)
                {
                    RaycastHit hit;
                    Ray castRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(castRay, out hit, 500, ~ignoreMask))
                    {
                        if (hit.transform.tag == "Enemy")
                        {
                            Debug.Log("Enemy: " + hit.transform.name);
                            targetIndicator.transform.parent = hit.transform;
                            targetIndicator.transform.localPosition = new Vector3(0, -.5f, 0);
                            targetIndicator.transform.localEulerAngles = new Vector3(-90, 0, 0);
                            targetIndicator.GetComponent<Renderer>().material.color = new Color(0, 255, 0, 1);
                            target = hit.transform.gameObject;
                            hitPoint = target.transform.position;
                        }
                        else
                        {
                            hitPoint = hit.point;
                            target = null;
                            targetIndicator.transform.parent = null;
                            targetIndicator.GetComponent<Renderer>().material.color = new Color(0, 255, 0, 0);
                        }
                    }
                }
            }
            float minDist = 1f;

            if (target == null)
            {
                agent.SetDestination(hitPoint);
            }
            else
            {
                meshAnimator.SetFloat("delayAttack", meshAnimator.GetFloat("delayAttack") - Time.deltaTime);
                minDist = attackRange;
                agent.SetDestination(target.transform.position);
                hitPoint = target.transform.position;
            }

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(hitPoint.x, 0, hitPoint.z)) > minDist)
            {
                meshAnimator.SetBool("isRunning", true);
                agent.updatePosition = true;
                meshAnimator.SetBool("isAttacking", false);
            }
            else
            {
                meshAnimator.SetBool("isRunning", false);
                agent.updatePosition = false;
                if (target != null)
                {
                    meshAnimator.SetBool("isAttacking", true);
                }
                else
                {
                    meshAnimator.SetBool("isAttacking", false);
                }
            }

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitPoint, .5f);
    }

    void AttackEnemy()
    {
        if(Vector3.Distance(transform.position, hitPoint) <= attackRange && !meshAnimator.GetBool("isDead"))
        {
            meshAnimator.SetFloat("delayAttack", delayAttack);
            target.GetComponentInChildren<Stats>().TakeDamage(GetComponentInChildren<Stats>().strength);
        }
    }

    public void RemoveTarget(GameObject sender)
    {
        if (sender == target)
        {
            target = null;
            targetIndicator.transform.parent = null;
            targetIndicator.GetComponent<Renderer>().material.color = new Color(0, 255, 0, 0);
        }
    }
}
