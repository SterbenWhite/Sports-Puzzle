using UnityEngine;
using UnityEngine.UI;
public class Level_Button : MonoBehaviour
{
    public Standard_Mode standard_mode_system;
    public int LevelNumber; //Номер уровня в списке Standard_Mode
    public Text LevelName;
    public GameObject Lock_Icon;
    TimeMode_Timer timer;
    private void Awake()
    {
        timer = standard_mode_system.GetComponent<TimeMode_Timer>();
    }
    public void Select_Level()
    {
        if(GameManager.Current_Game_Mode == Game_Mode.Standard)
        {
            if (standard_mode_system.Levels_Standard[LevelNumber].Level_Status.UnLocked == true)
            {
                standard_mode_system.LevelsPanel.SetActive(false);
                standard_mode_system.StandardGame_Panel.StartLevel(LevelNumber);
            }
        }
        else if (GameManager.Current_Game_Mode == Game_Mode.Time)
            {
                if (standard_mode_system.Levels_TimeMode[LevelNumber].Level_Status.UnLocked == true)
                {
                    standard_mode_system.LevelsPanel.SetActive(false);
                    standard_mode_system.StandardGame_Panel.StartLevel(LevelNumber);
                }
            }
    } //Вызывает метод генерации уровня, если уровень открыт
    
}
