using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Puzzle_Fragment : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Standard_Mode standardmode_system;
    public int FragmentNumber;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.Dragged.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        GameManager.Dragged.ElementNumber = FragmentNumber;
        GameManager.Dragged.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        GameManager.Dragged.GetComponent<Image>().enabled = true;
        GameManager.Dragged.Selected_Fragment = gameObject;
            foreach (Puzzle_Slot slot in standardmode_system.Slots)
            {
                if (slot.RequireElementNumber != FragmentNumber&slot.Outline.activeSelf)
                {
                    slot.Outline.SetActive(false);
                }
            }
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameManager.Dragged.GetComponent<RectTransform>().anchoredPosition += eventData.delta / GameManager.Dragged.transform.parent.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.Dragged.GetComponent<Image>().enabled = false;
        standardmode_system.CheckPuzzle();

    }
    public void OpenTipPanel()
    {
        if (GameManager.Stats.Tips > 0&!GameManager.Dragged.GetComponent<Image>().enabled&GameManager.Current_Game_Mode != Game_Mode.Custom)
        {
            standardmode_system.UseTip_Panel.SetActive(true);
            GameManager.Dragged.ElementNumber = FragmentNumber;
        }
    }
}
