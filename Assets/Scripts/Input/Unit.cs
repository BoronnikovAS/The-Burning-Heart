using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GameObject selectedGameobject;
    private Move movePosition;

    private void Awake()
    {
        selectedGameobject = transform.Find("Selected").gameObject;
        movePosition = GetComponent<Move>();
        SetSelectedVisible(false);
    }
    public void SetSelectedVisible (bool visible)
    {
        selectedGameobject.SetActive(visible);
    }
    public void MoveTo (Vector3 targetPosition)
    {
        movePosition.TriggerPosition(targetPosition);
    }    
}
