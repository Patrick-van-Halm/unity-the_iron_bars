using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class MovableImageRect : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float lineWidth = 10;

    public RectTransform imageRectTransform;


    public void UpdateTransform()
    {
        Vector3 differenceVector = pointB - pointA;
        imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineWidth);
        imageRectTransform.pivot = new Vector2(0, 0.5f);
        imageRectTransform.position = pointA;
        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        imageRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}