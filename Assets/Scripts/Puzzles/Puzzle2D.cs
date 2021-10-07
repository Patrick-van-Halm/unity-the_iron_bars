using UnityEngine;
using UnityEngine.Events;

public enum PuzzleState
{
    NotStarted,
    Started,
    Finished
}

public abstract class Puzzle2D : MonoBehaviour, IRaycastable3D
{
    [Header("Puzzle2D Settings")]
    public float interactionRange;
    public GameObject canvas;

    [Header("Puzzle2D Events")]
    public UnityEvent OnPuzzleFinished;

    public PuzzleState PuzzleState { get; private set; }
    public bool IsOpen => canvas.activeSelf;

    private PlayerController controller;

    protected virtual void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    protected virtual void Toggle() 
    {
        controller.SetCCEnabled(false);
        canvas.SetActive(!IsOpen);
        if (IsOpen && PuzzleState == PuzzleState.NotStarted) PuzzleState = PuzzleState.Started;
        if (IsOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    protected void Finish()
    {
        PuzzleState = PuzzleState.Finished;
        OnPuzzleFinished?.Invoke();
    }

    public virtual void OnRaycastStay(RaycastHit hit)
    {
        if (hit.distance > interactionRange) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        Toggle();
    }
    public virtual void OnRaycastEnter(RaycastHit hit){}
    public virtual void OnRaycastExit(){}
}