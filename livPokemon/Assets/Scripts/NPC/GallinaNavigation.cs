using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GallinaNavigation : MonoBehaviour
{
    private NavMeshAgent navAgent;

    public float patrol_Radius = 30f;
    public float patrol_Timer = 10f;
    public float speed = 4.0f;
    private float timer_Count;

    public GameObject target;

    bool PlayerClose = false;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        timer_Count = patrol_Timer;
    }

    void Update()
    {
        if (!PlayerClose)
        {
            Patrol();
        }
        else
        {
            RunAway();
        }
    }

    void Patrol()
    {
        timer_Count += Time.deltaTime;

        if (timer_Count> patrol_Timer)
        {
            SetNewRandomDestination();

            timer_Count = 0f;
        }

        if (navAgent.velocity.sqrMagnitude ==0)
        {
            //animation walk false
        }
        else
        {
            //animation walk true
        }
    }

    void SetNewRandomDestination()
    {
        Vector3 newDestination = RandomNavSphere(transform.position, patrol_Radius, -1);

        navAgent.Move(transform.forward * Time.deltaTime);
        navAgent.SetDestination(newDestination);
    }
    Vector3 RandomNavSphere(Vector3 originPos, float radius, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * radius;
        randDir += originPos;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, radius, layerMask);

        return navHit.position;
    }

    void RunAway()
    {
        Vector3 direction = transform.position-target.transform.position;
        Vector3 newPos = transform.position + direction;

        navAgent.speed = speed;
        navAgent.Move(transform.forward * Time.deltaTime);
        navAgent.SetDestination(newPos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "granjero")
        {
            PlayerClose = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "granjero")
        {
            PlayerClose = false;
        }
    }
}
