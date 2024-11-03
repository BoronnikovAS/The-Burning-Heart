using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public float speed;

    private Vector3 tPosition;
    private bool isMooving = false;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    void Update()
    {    
        if (isMooving)
        {
            ItsMove();
        }
    }
    public void TriggerPosition(Vector3 tPosition)
    {
        this.tPosition = tPosition;
        tPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tPosition.z = transform.position.z;

        isMooving = true;
    }

    void ItsMove()
    {                
        Vector3 Look = transform.InverseTransformPoint(tPosition);
        float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, Angle);
        //transform.position = Vector3.MoveTowards(transform.position, tPosition, speed * Time.deltaTime);
        navMeshAgent.SetDestination(tPosition);
        if (transform.position == tPosition)
        {
            isMooving = false;
        }
    }
}
