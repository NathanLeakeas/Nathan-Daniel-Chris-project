using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField]
    float maxCooldown = 2.5f;
    float cooldown;

    //The list of all patrol nodes to visit.
    [SerializeField]
    public List<Waypoint> _patrolPoints;

    //Private variables for base behavior.
    NavMeshAgent agent;
    int _currentPatrolIndex;
    bool _traveling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;
    public AudioSource barrelSound;
    public ParticleSystem muzzleFlash;

    //Player
    public GameObject target;


    // Start is called before the first frame update
    public void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        cooldown = 0f;
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
        SetDestination();
        cooldown -= Time.deltaTime;
        /**
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
        /*/
    }

    private void SetDestination()
    {
        Vector3 targetVector = target.transform.position;
        Vector3 targetPos = transform.InverseTransformPoint(target.transform.position);
        Vector3 agentPos = transform.InverseTransformPoint(agent.transform.position);
        float dist = Mathf.Sqrt(Mathf.Pow(transform.InverseTransformPoint(target.transform.position).x, 2) + Mathf.Pow(transform.InverseTransformPoint(target.transform.position).z, 2)) - Mathf.Sqrt(Mathf.Pow(agentPos.x, 2) + Mathf.Pow(agentPos.z, 2));
        if (dist <= 15f && dist >= 8f)
        {
            agent.SetDestination(target.transform.position);
        }

        //Go back to patrolling if player is not close.
        else if (dist > 15f)
        {
            agent.SetDestination(_patrolPoints[_currentPatrolIndex].transform.position);
            
            if(agent.remainingDistance<1f)
            {
                ChangePatrolPoint();
                //Debug.Log(_patrolPoints[_currentPatrolIndex]);
            }
            //_traveling = true;
        }

        else if (dist < 8f)
        {
            agent.ResetPath();
            this.transform.LookAt(target.transform);
            if (cooldown <= 0)
            {
                Shoot();
            }
        }
    }

    private void ChangePatrolPoint()
    {
        /*/
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }
        /*/

        

        if (_currentPatrolIndex == (_patrolPoints.Count-1))
        {
            _currentPatrolIndex = 0;
        }

        else
        {
            _currentPatrolIndex += 1;
            //Debug.Log(_currentPatrolIndex);
            //Debug.Log(_patrolPoints.Count);
        }
    }


    private void Shoot()
    {
        //turn to face player
        cooldown = maxCooldown;
        Vector3 shootAdjustAngle = new Vector3(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f));
        RaycastHit hit;
        barrelSound.Play();
        muzzleFlash.Play();
        if (Physics.Raycast(this.transform.position, this.transform.forward + shootAdjustAngle, out hit, 30f))
        {
            target.GetComponent<PlayerStats>().takeDamage(10);

        }
        //Debug.Log("Shooting...");



    }

}
