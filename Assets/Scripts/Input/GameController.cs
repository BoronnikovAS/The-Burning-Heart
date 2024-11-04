using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GUISkin skin;
    public static List<Unit> selectedUnitList;
    
    private Vector2 startPos;
    private Vector2 endPos;    
    private Rect rect;
    private bool draw;    

    private void Awake()
    {
        selectedUnitList = new List<Unit>();        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            endPos = Input.mousePosition;
            Vector2 moveToPosition = Camera.main.ScreenToWorldPoint(endPos);
            List<Vector2> targetPositionList = GetPositionListAround(moveToPosition, new float[] {2f,4f,6f}, new int[] { 5, 10, 20});

            int targetPositionListIndex = 0;
            foreach (Unit unit in selectedUnitList)
            {
                unit.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }
        }
      
    }
    
    private void OnGUI()
    {
        GUI.skin = skin;
        GUI.depth = 99;

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            draw = true;
        }

        if (Input.GetMouseButtonUp(0))
        {            
            Vector2 startPosWorld = Camera.main.ScreenToWorldPoint(startPos);
            Vector2 endPosWorld = Camera.main.ScreenToWorldPoint(endPos);
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosWorld, endPosWorld);
            //Отмена выбора всех юнитов
            foreach (Unit unit in selectedUnitList)
            {
                unit.SetSelectedVisible(false);
            }
            selectedUnitList.Clear();
            //Выбор всех юнитов в выбранной площаде 
            foreach (Collider2D collider2D in collider2DArray)
            {
                Unit unit = collider2D.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.SetSelectedVisible(true);
                    selectedUnitList.Add(unit);
                }
            }           
            draw = false;
        }

        if (draw == true)
        {
            endPos = Input.mousePosition;
            if (startPos == endPos) return;

            rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                            Screen.height - Mathf.Max(endPos.y, startPos.y),
                            Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                            Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
                            );

            GUI.Box(rect, "");
        }        
    }
    private List<Vector2> GetPositionListAround(Vector2 startPosition, float[] ringDistanceArray, int[] ringPositionalCountArray)
    {
        List<Vector2> positionList = new List<Vector2>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionalCountArray[i]));
        }
        return positionList;
    }
    private List<Vector2> GetPositionListAround(Vector2 startPosition, float distance, int positionalCount)
    {
        List<Vector2> positionList = new List<Vector2>();
        for (int i = 0; i < positionalCount; i++)
        {
            float anlge = i * (360f / positionalCount);
            Vector2 dir = ApplyRotationVector(new Vector2(1, 0), anlge);
            Vector2 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }
    private Vector2 ApplyRotationVector(Vector2 vec, float anlge)
    {
        return Quaternion.Euler(0, 0, anlge) * vec;        
    }
}
