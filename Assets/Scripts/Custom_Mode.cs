using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Custom_Mode : MonoBehaviour
{

    public Sprite None_Sprite;
    public Image Image_Slot;
    public GridLayoutGroup PuzzleSlots_Grid;
    public Transform PuzzleFragments_Content;
    public GameObject Import_Panel;
    public GameObject Game_Panel;
    public GameObject Victory_Panel;
    public Button Create_Button;
    public Text GridSize_Text;
    public Button AddGrid_Button;
    public Button RemoveGrid_Button;
    public Puzzle_Slot PuzzleSlot_Prefab;
    public Puzzle_Fragment PuzzleFragment_Prefab;
    public List<Size_Variant> Puzzle_Size_Variants = new List<Size_Variant>();
    int current_size_number = 0;
    Standard_Mode standard_mode;
    private void Start()
    {
        standard_mode = GetComponent<Standard_Mode>();
    }
    public void Check_Import_Settings()
    {
        if (Image_Slot.sprite != None_Sprite & current_size_number >= 0 & current_size_number < Puzzle_Size_Variants.Count)
        {
            Create_Button.interactable = true;
        }
        else
        {
            Create_Button.interactable = false;
        }
        GridSize_Text.text = $"{Puzzle_Size_Variants[current_size_number].GridSize}x{Puzzle_Size_Variants[current_size_number].GridSize}";
    } //Включает кнопку создания пазла, если настройки верные 
    public void Clear_Import_Settings() //Очищает настройки пазла
    {
        Image_Slot.sprite = None_Sprite;
        current_size_number = 0;
        AddGrid_Button.interactable = true;
        RemoveGrid_Button.interactable = false;
        Check_Import_Settings();
        Game_Panel.SetActive(false);
        Victory_Panel.SetActive(false);
        Import_Panel.SetActive(true);
        standard_mode.GameClear();
    }
    public void AddGridSize(int size)
    {
        current_size_number += size;
        if (current_size_number + size < 0)
        {
            RemoveGrid_Button.interactable = false;
        }
        else
        {
            RemoveGrid_Button.interactable = true;
        }
        if (current_size_number + size >= Puzzle_Size_Variants.Count)
        {
            AddGrid_Button.interactable = false;
        }
        else
        {
            AddGrid_Button.interactable = true;
        }
        Check_Import_Settings();
    }
    public void Create_Puzzle()
    {
        GameManager.Current_Game_Mode = Game_Mode.Custom;
        Victory_Panel.SetActive(false);
        Import_Panel.SetActive(false);
        standard_mode.GameClear();
        PuzzleSlots_Grid.cellSize = CurrentSizeVariant().ElementsSize;
        Sprite[] fragments_sprites = SpriteSplitter.SplitTexture(Image_Slot.sprite.texture, CurrentSizeVariant().GridSize, CurrentSizeVariant().GridSize);
        for (int i = 0; i < CurrentSizeVariant().GridSize * CurrentSizeVariant().GridSize; i++)
        {
            Puzzle_Slot new_slot = Instantiate(PuzzleSlot_Prefab, PuzzleSlots_Grid.transform);
            Puzzle_Fragment new_fragment = Instantiate(PuzzleFragment_Prefab, PuzzleFragments_Content);
            new_fragment.FragmentNumber = i;
            new_fragment.GetComponent<Image>().sprite = fragments_sprites[i];
            new_slot.RequireElementNumber = i;
            new_fragment.standardmode_system = standard_mode;
            standard_mode.Slots.Add(new_slot);
            standard_mode.Fragments.Add(new_fragment);

        }
        for (int i = 0; i < CurrentSizeVariant().GridSize * CurrentSizeVariant().GridSize; i++)
        {
            standard_mode.Fragments[i].transform.SetSiblingIndex(Random.Range(0, standard_mode.Fragments.Count - 1));
        }
        Game_Panel.SetActive(true);
    }
    public void CheckPuzzle()
    {
        bool all_filled = true;
        foreach (Puzzle_Slot slot in standard_mode.Slots)
        {
            if (slot.Filled == false)
            {
                all_filled = false;
                break;
            }
        }
        if (all_filled)
        {
            Victory();
        }
    } //Проверяет заполненность слотов для пазла
    Size_Variant CurrentSizeVariant()
    {
        return Puzzle_Size_Variants[current_size_number];
    }
    public void CustomGame_Clear()
    {
        standard_mode.GameClear();
        Clear_Import_Settings();
    }
    void Victory()
    {
        Game_Panel.SetActive(false);
        Victory_Panel.SetActive(true);
    }   
}
[System.Serializable]
public class Size_Variant
{
    public int GridSize = 2; //При разбивки используется как "GridSize x GridSize"
    public Vector2 ElementsSize = new Vector3(150, 150); //Размер слотов пазла в GridLayoutGroup.cellSize
}
