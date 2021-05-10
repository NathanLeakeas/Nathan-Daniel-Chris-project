using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyMovement1 : MonoBehaviour
{
    //Dictates whether the agent waits on each node
    [SerializeField]
    bool _patrolWaiting;

    //The total time we wait at each node.
    [SerializeField]
    float _totalWaitTime = 3f;

    //The probability of switching direction
    [SerializeField]
    float _switchProbability = 0.2f;

    //The list of all patrol nodes to visit.
    [SerializeField]
    List<Waypoint> _patrolPoints;

    //Private variables for base behavior.
    NavMeshAgent agent;
    int _currentPatrolIndex;
    bool _traveling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;

    //Player
    public GameObject target;
    

    // Start is called before the first frame update
    public void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("The nav mesh agent component is now attached to " + gameObject.name);
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behavior.");
            }
        }

    }

    // Update is called once per frame
    public void Update()
    {
        //Check if we're close to the destination
        if (_traveling && agent.remainingDistance <= 1.0f)
        {
            _traveling = false;

            //if we're going to wait, then wait.
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }

        }

        //Instead if we're waiting
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        Vector3 targetVector = target.transform.position;
        Vector3 targetPos = transform.InverseTransformPoint(target.transform.position);
        Vector3 agentPos = transform.InverseTransformPoint(agent.transform.position);
        float dist = Mathf.Sqrt(Mathf.Pow(targetPos.x, 2) + Mathf.Pow(targetPos.z, 2)) - Mathf.Sqrt(Mathf.Pow(agentPos.x, 2) + Mathf.Pow(agentPos.z, 2));
        if (dist<=15 && dist >= 8)
        {
            agent.SetDestination(targetVector);
        }
        
        //Go back to patrolling if player is not close.
        else if (dist>15)
        { 
            if (_patrolPoints != null)
            {
                targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
                agent.SetDestination(targetVector);
                _traveling = true;
            }
        }

        else if (dist<8)
        {
            Shoot();
        }
    }   

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }

        if (_patrolForward)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }

        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }


    private void Shoot()
    {
        //turn to face player
        this.transform.LookAt(target.transform);
        Vector3 shootAdjustAngle = new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f));
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 30f))
        { 
            //reduce player health
        }


    }

}
