using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingUI : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    [SerializeField] float rngX;
    [SerializeField] float rngY;

    private void LateUpdate()
    {
        float ttPosX = targetObj.transform.position.x - rngX;
        float ttPosY = targetObj.transform.position.y- rngY;
        transform.position = new Vector3(ttPosX, ttPosY, transform.position.z);
    }
}
