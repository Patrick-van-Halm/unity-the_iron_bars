using UnityEngine;
using UnityEngine.Events;

public enum PuzzleState
{
    NotStarted,
    Started,
    Finished
}

public abstract class Puzzle2D : Interactable
{
    [Header("Puzzle2D Settings")]
    public float interactionRange;
    public GameObject canvas;

    [Header("Puzzle2D Events")]
    public UnityEvent OnPuzzleFinished;

    public PuzzleState PuzzleState { get; private set; }
    public bool IsOpen => canvas.activeSelf;

    private PlayerController controller;

    protected override void Start()
    {
        base.Start();
        controller = FindObjectOfType<PlayerController>();
    }

    protected override void Interact()
    {
        Toggle();
    }

    public virtual void Toggle() 
    {
        canvas.SetActive(!IsOpen);
        controller.SetCanMove(!IsOpen);
        if (IsOpen && PuzzleState == PuzzleState.NotStarted) PuzzleState = PuzzleState.Started;
    }

    protected void Finish()
    {
        PuzzleState = PuzzleState.Finished;
        OnPuzzleFinished?.Invoke();
    }
}