using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Standard_Mode : MonoBehaviour
{
    public Transform Content; //Transform в котором спавнятся кнопки уровней
    public Level_Button LevelButton_Prefab; //Префаб кнопки уровня, который спавнится при старте
    public List<Level> Levels_Standard = new List<Level>();
    public List<TimeMode_Level> Levels_TimeMode = new List<TimeMode_Level>();
    public GameObject LevelsPanel;
    public Standard_Game StandardGame_Panel;
    public GameObject Control_Panel;
    public GameObject Victory_Panel;
    public GameObject DefeatPanel;
    public GameObject UseTip_Panel;
    public Text RewardText;
    public Button NextLevelButton;

    List<Level_Button> spawned_level_buttons = new List<Level_Button>();
    [HideInInspector] public List<Puzzle_Slot> Slots = new List<Puzzle_Slot>(); //Текущие, заспавненные слоты
    [HideInInspector] public List<Puzzle_Fragment> Fragments = new List<Puzzle_Fragment>(); //Текущие, заспавненные фрагменты
    [HideInInspector] public GameManager Manager;
    private void Awake()
    {
        Manager = GetComponent<GameManager>();
        Generate_Level_Buttons();
    }
    
    public void UpdateLevels()
    {
        if(GameManager.Current_Game_Mode == Game_Mode.Standard)
        {
            for (int i = 0; i < Levels_Standard.Count; i++)
            {
                spawned_level_buttons[i].LevelNumber = i;
                spawned_level_buttons[i].standard_mode_system = this;
                if (Levels_Standard[i].Level_Status.UnLocked)
                {
                    spawned_level_buttons[i].Lock_Icon.SetActive(false);
                    spawned_level_buttons[i].LevelName.text = $"Level {i + 1}";
                }
                else
                {
                    spawned_level_buttons[i].Lock_Icon.SetActive(true);
                    spawned_level_buttons[i].LevelName.text = $"Lock";
                }
                spawned_level_buttons.Add(spawned_level_buttons[i]);
            }        
        }
        else if (GameManager.Current_Game_Mode == Game_Mode.Time)
        {
            for (int i = 0; i < Levels_TimeMode.Count; i++)
            {
                spawned_level_buttons[i].LevelNumber = i;
                spawned_level_buttons[i].standard_mode_system = this;
                if (Levels_TimeMode[i].Level_Status.UnLocked)
                {
                    spawned_level_buttons[i].Lock_Icon.SetActive(false);
                    spawned_level_buttons[i].LevelName.text = $"Level {i + 1}";
                }
                else
                {
                    spawned_level_buttons[i].Lock_Icon.SetActive(true);
                    spawned_level_buttons[i].LevelName.text = $"Lock";
                }
                spawned_level_buttons.Add(spawned_level_buttons[i]);
            }
        }
    } //Обновляет статус уровней
    void Generate_Level_Buttons()
    {
        for (int i = 0; i < spawned_level_buttons.Count; i++)
        {
            Destroy(spawned_level_buttons[i].gameObject);
        }
        spawned_level_buttons.Clear();
        if(GameManager.Current_Game_Mode == Game_Mode.Standard)
        {
            for (int i = 0; i < Levels_Standard.Count; i++)
            {
                Level_Button new_level_button = Instantiate(LevelButton_Prefab, Content);
                new_level_button.LevelNumber = i;
                new_level_button.standard_mode_system = this;
                if (Levels_Standard[i].Level_Status.UnLocked)
                {
                    new_level_button.Lock_Icon.SetActive(false);
                    new_level_button.LevelName.text = $"Level {i + 1}";
                }
                else
                {
                    new_level_button.LevelName.text = $"Lock";
                }
                spawned_level_buttons.Add(new_level_button);
            }
        }
        else if (GameManager.Current_Game_Mode == Game_Mode.Time)
        {
            for (int i = 0; i < Levels_TimeMode.Count; i++)
            {
                Level_Button new_level_button = Instantiate(LevelButton_Prefab, Content);
                new_level_button.LevelNumber = i;
                new_level_button.standard_mode_system = this;
                if (Levels_TimeMode[i].Level_Status.UnLocked)
                {
                    new_level_button.Lock_Icon.SetActive(false);
                    new_level_button.LevelName.text = $"Level {i + 1}";
                }
                else
                {
                    new_level_button.LevelName.text = $"Lock";
                }
                spawned_level_buttons.Add(new_level_button);
            }
        }
    } //Создаёт кнопки уровней, на основе списка уровней
    public void CheckPuzzle()
    {
        bool all_filled = true;
        foreach (Puzzle_Slot slot in Slots)
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
        UpdateLevels();
    } //Проверяет заполненность слотов для пазла
    void Victory()
    {
        UnlockNextLevel(StandardGame_Panel.current_level);
        Victory_Panel.gameObject.SetActive(true);
        Control_Panel.SetActive(false);
        UpdateLevels();

        if (Levels_Standard[StandardGame_Panel.current_level].Level_Status.Completed == false)
        {
            Reward(Levels_Standard[StandardGame_Panel.current_level].Level_Status.Reward);
            Levels_Standard[StandardGame_Panel.current_level].Level_Status.Completed = true;
        }
        else
        {
            RewardText.gameObject.SetActive(false);
        }

    }
    public void Defeat()
    {
        Control_Panel.SetActive(false);
        DefeatPanel.SetActive(true);
        GameManager.Dragged.GetComponent<Image>().enabled = false;
        Manager.AddLifes(-1);
    }
    public void UseTip()
    {
        if (GameManager.Stats.Tips > 0)
        {
            foreach(Puzzle_Slot slot in Slots)
            {
                if (slot.RequireElementNumber == GameManager.Dragged.ElementNumber)
                {
                    slot.Outline.SetActive(true);
                    UseTip_Panel.SetActive(false);
                    Manager.AddTips(-1);
                    break;
                }
            }
        }
    }
    void UnlockNextLevel(int cur_level)
    {
        if(GameManager.Current_Game_Mode == Game_Mode.Standard)
        {
            if (cur_level + 1 < Levels_Standard.Count)
            {
                Levels_Standard[cur_level + 1].Level_Status.UnLocked = true;
                NextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                NextLevelButton.gameObject.SetActive(false);
            }
        }
        else if (GameManager.Current_Game_Mode == Game_Mode.Time)
        {
            if (cur_level + 1 < Levels_Standard.Count)
            {
                Levels_TimeMode[cur_level + 1].Level_Status.UnLocked = true;
                NextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                NextLevelButton.gameObject.SetActive(false);
            }
        }
    }
    void Reward(int amount)
    {
        RewardText.gameObject.SetActive(true);
        Manager.AddMoney(amount);
        RewardText.text = $"You get {amount}";
    }
    public void GameClear()
    {
        Control_Panel.SetActive(true);
        DefeatPanel.SetActive(false);
        for (int i = 0; i < Slots.Count; i++)
        {
            Destroy(Slots[i].gameObject);
            Destroy(Fragments[i].gameObject);
        }
        Slots.Clear();
        Fragments.Clear();
    } //Очищает игровое поле, для следующего уровня
}
[System.Serializable]
public class Level
{
    public Texture2D Picture;
    public int GridSize = 2; //При разбивки используется как "GridSize x GridSize"
    public Vector2 ElementsSize = new Vector3(150, 150); //Размер слотов пазла в GridLayoutGroup.cellSize
    public Level_Mode_Status Level_Status;
}
[System.Serializable]
public class TimeMode_Level : Level
{
    public Timer TimeForGame; //Время на прохождение для TimeMode режима
}
[System.Serializable]
public class Timer
{
    public int Minutes;
    public float Seconds;
    public Timer(int min, float sec)
    {
        Minutes = min;
        Seconds = sec;
    }
    public bool TimeOut()
    {
        if (Seconds <= 0 & Minutes <= 0)
        {
            return true;
        }
        else return false;
    }
}
[System.Serializable]
public class Level_Mode_Status
{
    public bool UnLocked; //Если true - позволяет запустить этот уровень 
    [HideInInspector] public bool Completed; //Если false - позволяет получить награду за прохождение
    public int Reward = 4; //Награда за первое прохождение уровня
}

