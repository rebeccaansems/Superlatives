using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollRectSnap : MonoBehaviour
{

    public RectTransform panel;
    public GameObject rectContent;
    public GameObject[] snapObjects;
    public RectTransform center;

    private float[] distance;
    private bool isDragging = false;
    private int objectDistance;
    private int minObjectNum;

    void Setup()
    {
        TMP_InputField[] tempSnapObjects = rectContent.GetComponentsInChildren<TMP_InputField>();
        snapObjects = new GameObject[tempSnapObjects.Length];

        for (int i=0; i< tempSnapObjects.Length; i++)
        {
            snapObjects[i] = tempSnapObjects[i].transform.parent.gameObject;
        }

        distance = new float[snapObjects.Length];

        objectDistance = (int)Mathf.Abs(snapObjects[1].GetComponent<RectTransform>().anchoredPosition.x - snapObjects[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    void Update()
    {
        if(rectContent.GetComponentsInChildren<TMP_InputField>().Length != 0)
        {
            if(snapObjects.Length == 0)
            {
                Setup();
            }

            for (int i = 0; i < snapObjects.Length; i++)
            {
                distance[i] = Mathf.Abs(center.transform.position.x - snapObjects[i].transform.position.x - 2);
            }

            float minDistance = Mathf.Min(distance);

            for (int i = 0; i < snapObjects.Length; i++)
            {
                if (minDistance == distance[i])
                {
                    minObjectNum = i;
                    break;
                }
            }

            if (!isDragging)
            {
                LerpToNextObject((minObjectNum * -objectDistance) - objectDistance / 2);
            }
        }
    }

    void LerpToNextObject(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 5f);
        Vector2 newPos = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPos;
    }

    public void StartDrag()
    {
        isDragging = true;
    }

    public void StopDrag()
    {
        isDragging = false;
    }
}
