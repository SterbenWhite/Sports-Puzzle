using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Puzzle_Slot : MonoBehaviour, IDropHandler
{
    public int RequireElementNumber;
    public bool Filled;
    public GameObject Outline;
    Image thisImage;
    public void OnDrop(PointerEventData eventData)
    {
        if (GameManager.Dragged.ElementNumber == RequireElementNumber & Filled == false&GameManager.Dragged.GetComponent<Image>().enabled)
        {
            thisImage = GetComponent<Image>();
            Filled = true;
            thisImage.sprite = GameManager.Dragged.GetComponent<Image>().sprite;
            GameManager.Dragged.Selected_Fragment.SetActive(false);
            GameManager.Dragged.GetComponent<Image>().enabled = false;
            if (GameManager.Current_Game_Mode != Game_Mode.Custom)
            {
                GameManager.Dragged.Selected_Fragment.GetComponent<Puzzle_Fragment>().standardmode_system.CheckPuzzle();
            }
            else
            {
                GameManager.Dragged.Selected_Fragment.GetComponent<Puzzle_Fragment>().standardmode_system.GetComponent<Custom_Mode>().CheckPuzzle();  
            }
        }
        Outline.SetActive(false);
    }
}
