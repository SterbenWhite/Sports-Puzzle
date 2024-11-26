using DG.Tweening;
using UnityEngine;
public class Window_Anim : MonoBehaviour
{
    public Vector3 Start_Position;
    public Vector3 Target_Position;
    public float Duration = 1;
    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        rect.anchoredPosition = Start_Position;
        rect.DOAnchorPos(Target_Position, Duration);
    }

}
