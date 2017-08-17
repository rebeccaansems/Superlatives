using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Copyright (C) Xenfinity LLC - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bilal Itani <bilalitani1@gmail.com>, June 2017
 */

public class UIElementDragger : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "Draggable";
    public GameObject nameBlockHeader;
    public VerticalLayoutGroup vert;

    private bool dragging = false;

    private Vector2 originalPosition;
    private Vector3 offset;
    private Transform objectToDrag;
    private Image objectToDragImage;

    private List<string> playerOrder = new List<string>();

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    private void Start()
    {
        for (int i = 0; i < Global.allPlayers.Count; i++)
        {
            playerOrder.Add(nameBlockHeader.transform.GetChild(i).name);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if (objectToDrag != null)
            {
                dragging = true;

                objectToDrag.SetAsLastSibling();

                offset = objectToDrag.transform.position - Input.mousePosition;
                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Image>();
                objectToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            objectToDrag.position = Input.mousePosition + offset;
            vert.enabled = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null)
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToReplace.position = originalPosition;

                    int replaceNumber = playerOrder.IndexOf(objectToReplace.name);
                    playerOrder[playerOrder.IndexOf(objectToDrag.name)] = objectToReplace.name;
                    playerOrder[replaceNumber] = objectToDrag.name;
                }
                else
                {
                    objectToDrag.position = originalPosition;
                }

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
        }
    }

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        var clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }

    public List<string> GetPlayerListOrder()
    {
        playerOrder.Reverse();
        playerOrder.Add(PhotonNetwork.player.CustomProperties["JoinNumber"].ToString());
        return playerOrder;
    }
}
