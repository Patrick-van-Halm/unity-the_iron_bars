using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using Extensions;

public class Wires : Puzzle2D
{
    [Header("Wires Settings")]
    [Range(3, 10)]
    public int minWireCount = 3;
    public List<Color> colors = new List<Color>();
    [Header("Wire Node Settings")]
    public Transform startNodes;
    public Transform endNodes;
    public Transform wireImages;

    private byte[,] grid;
    private Wire[] wires;
    private int wireCount;

    protected override void Start()
    {
        base.Start();

        GeneratePuzzle();
    }

    private void ClearPuzzle()
    {
        startNodes.DeleteChildren();
        endNodes.DeleteChildren();
        wireImages.DeleteChildren();
        wires = null;
        grid = null;
    }

    public void GeneratePuzzle()
    {
        PuzzleState = PuzzleState.NotStarted;
        ClearPuzzle();

        var rng = new System.Random();
        wireCount = rng.Next(minWireCount, colors.Count);

        grid = new byte[wireCount, 2];
        wires = new Wire[wireCount];

        var randomGrid = new byte[wireCount];
        for (int i = 0; i < wireCount; i++)
            randomGrid[i] = (byte)i;

        rng.Shuffle(randomGrid);
        for (int i = 0; i < wireCount; i++)
        {
            grid[i, 1] = (byte)i;
            grid[i, 0] = randomGrid[i];

            wires[i] = new Wire();

            var node = new GameObject();
            node.name = "StartNode[" + i + "]";
            node.transform.SetParent(startNodes);
            node.transform.localScale = Vector3.one;
            node.transform.position = Vector3.zero;
            var startNode = node;
            node.AddComponent<RectTransform>();
            node.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
            node.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);
            node.AddComponent<RawImage>();

            node = new GameObject();
            node.name = "MoveNode[" + i + "]";
            node.transform.SetParent(startNode.transform);
            node.transform.localScale = Vector3.one;
            node.transform.position = Vector3.zero;

            wires[i].movableNode = node.AddComponent<RectTransform>();
            wires[i].movableNode.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
            wires[i].movableNode.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);

            node = new GameObject();
            node.name = "EndNode[" + i + "]";
            node.transform.SetParent(endNodes);
            node.transform.localScale = Vector3.one;
            node.transform.position = Vector3.zero;
            node.AddComponent<RectTransform>();
            node.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20);
            node.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);
            wires[i].endNodeImage = node.AddComponent<RawImage>();

            node = new GameObject();
            node.name = "MoveImage[" + i + "]";
            node.transform.SetParent(wireImages);
            node.transform.localScale = Vector3.one;
            node.transform.position = Vector3.zero;

            node.AddComponent<RectTransform>();
            wires[i].wireImage = node.AddComponent<RawImage>();

            wires[i].movableImage = node.AddComponent<MovableImageRect>();
            wires[i].movableImage.imageRectTransform = node.GetComponent<RectTransform>();
        }

        for (int i = 0; i < wireCount; i++)
        {
            wires[i].allEndNodes = endNodes.GetComponentsOnlyInChildren<RectTransform>();
            wires[i].startNodeImage = startNodes.GetChild(grid[i, 0]).GetComponent<RawImage>();
            wires[i].SetColor(colors[i]);
            wires[i].ResetTransforms();
        }
    }

    private void Update()
    {
        if (PuzzleState != PuzzleState.Started) return;

        if(wires.Any(w => w.IsHolding))
        {
            wires.First(w => w.IsHolding).Update();
        }
        else
        {
            for(int i = 0; i < wireCount; i++)
            {
                wires[i]?.Update();
            }
        }

        if(wires.All(w => w != null && w.IsConnectedCorrectly))
        {
            Finish();
        }
    }
}
