using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Move : MonoBehaviour
{
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
        Vector3 Look = transform.InverseTransformPoint(tPosition);
        float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;        
        transform.Rotate(0, 0, Angle);
        isMooving = true;
        ItsMove();
    }

    void ItsMove()
    {
        navMeshAgent.SetDestination(tPosition);        
        float posX = System.MathF.Round(transform.position.x, 2);
        float posY = System.MathF.Round(transform.position.y, 2);
        float tposX = System.MathF.Round(tPosition.x, 2);
        float tposY = System.MathF.Round(tPosition.y, 2);        
        if (posX == tposX && posY == tposY)
        {            
            isMooving = false;            
        }        
    }
}
