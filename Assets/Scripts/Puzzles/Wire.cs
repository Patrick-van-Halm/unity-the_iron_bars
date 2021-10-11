using Railbound.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Wire
{
    public RawImage wireImage;
    public RawImage startNodeImage;
    public RawImage endNodeImage;
    public RectTransform movableNode;
    public RectTransform[] allEndNodes;
    public MovableImageRect movableImage;

    public bool IsConnectedCorrectly => connectedNode == endNodeImage.rectTransform && !IsHolding;
    public bool IsHolding { get; private set; }

    private RectTransform connectedNode;

    public void SetColor(Color color)
    {
        wireImage.color = color;
        endNodeImage.color = color;
        startNodeImage.color = color;
    }

    public void ResetTransforms()
    {
        movableNode.position = startNodeImage.rectTransform.position;
        connectedNode = null;

        movableImage.pointA = movableNode.position;
        movableImage.pointB = movableNode.position;
        movableImage.UpdateTransform();
    }

    // Update is called once per frame
    public void Update()
    {
        //if (!movableNode.ContainsPosition(Input.mousePosition, true) && !isHolding) return;
        if (movableNode.ContainsPosition(Mouse.current.position.ReadValue(), true) && Mouse.current.leftButton.ReadValue() == 1 && !IsHolding) IsHolding = true;
        else if (Mouse.current.leftButton.ReadValue() == 0 && IsHolding) IsHolding = false;

        if (IsHolding)
        {
            movableNode.position = Mouse.current.position.ReadValue();

            connectedNode = null;
            for (int i = 0; i < allEndNodes.Length; i++)
            {
                var node = allEndNodes[i];
                if (node.ContainsPosition(Mouse.current.position.ReadValue()))
                {
                    connectedNode = node;
                    break;
                }
            }
        }
        else if(!IsHolding && connectedNode)
        {
            movableNode.position = connectedNode.position;
        }
        else if (!IsHolding && !connectedNode)
        {
            movableNode.position = startNodeImage.rectTransform.position;
        }

        movableImage.pointA = startNodeImage.rectTransform.position;
        movableImage.pointB = movableNode.position;
        movableImage.UpdateTransform();
    }
}
