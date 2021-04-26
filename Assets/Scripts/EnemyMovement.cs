using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    NavMeshAgent agent;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        //SetDestination(target);
    }


    void Update()
    {
        SetDestination();
        Debug.Log("Updated");
    }

    // Update is called once per frame
    void SetDestination()
    {
        Vector3 targetVector = target.transform.position;
        agent.SetDestination(targetVector);
    }
}
