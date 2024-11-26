using UnityEngine;
using UnityEngine.UI;

public class Standard_Game : MonoBehaviour
{
    public Text LevelName;
    public Standard_Mode Standard_Mode_System;
    public GridLayoutGroup PuzzleSlots_Grid;
    public Transform PuzzleFragments_Content;
    public Puzzle_Slot PuzzleSlot_Prefab;
    public Puzzle_Fragment PuzzleFragment_Prefab;
    [HideInInspector] public int current_level;
    public TimeMode_Timer timer;
    Level level;
    public void StartLevel(int levelNumber)
    {
        Standard_Mode_System.Victory_Panel.SetActive(false);
        Standard_Mode_System.DefeatPanel.SetActive(false);
        current_level = levelNumber;
        if(GameManager.Current_Game_Mode == Game_Mode.Standard)
        {
            level = Standard_Mode_System.Levels_Standard[levelNumber];
        }
        else if (GameManager.Current_Game_Mode == Game_Mode.Time)
        {
            level = Standard_Mode_System.Levels_TimeMode[levelNumber];
        }
        LevelName.text = $"Level {levelNumber + 1}";
        PuzzleSlots_Grid.cellSize = level.ElementsSize;
        Sprite[] fragments_sprites = SpriteSplitter.SplitTexture(level.Picture, level.GridSize, level.GridSize);
        for (int i = 0; i < level.GridSize * level.GridSize; i++)
        {
            Puzzle_Slot new_slot = Instantiate(PuzzleSlot_Prefab, PuzzleSlots_Grid.transform);
            Puzzle_Fragment new_fragment = Instantiate(PuzzleFragment_Prefab, PuzzleFragments_Content);
            new_fragment.FragmentNumber = i;
            new_fragment.GetComponent<Image>().sprite = fragments_sprites[i];
            new_slot.RequireElementNumber = i;
            new_fragment.standardmode_system = Standard_Mode_System;
            Standard_Mode_System.Slots.Add(new_slot);
            Standard_Mode_System.Fragments.Add(new_fragment);

        }
        for (int i = 0; i < level.GridSize * level.GridSize; i++)
        {
            Standard_Mode_System.Fragments[i].transform.SetSiblingIndex(Random.Range(0, Standard_Mode_System.Fragments.Count - 1));
        }
        if (GameManager.Current_Game_Mode == Game_Mode.Time)
        {
            timer.Time_Remaining = new Timer(Standard_Mode_System.Levels_TimeMode[current_level].TimeForGame.Minutes, Standard_Mode_System.Levels_TimeMode[current_level].TimeForGame.Seconds);
            timer.enabled = true;
            timer.TimerText.gameObject.SetActive(true);
        }
        else
        {
            timer.TimerText.gameObject.SetActive(false);
            timer.enabled = false;
        }
        gameObject.SetActive(true);
    } //√енерирует новый уровень, под номером из списка уровней.

    public void NextLevel()
    {
        Standard_Mode_System.GameClear();
        StartLevel(current_level + 1);
    } //«апускает следующий уровень
    private void OnDisable()
    {
        Standard_Mode_System.GameClear();
    }
}