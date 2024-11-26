using UnityEngine;
using UnityEngine.UI;
public class TimeMode_Timer : MonoBehaviour
{
    public Standard_Mode standard_mode;
    public Timer Time_Remaining = new Timer(0, 0); //Оставшееся время для прохождения (Time Mode)
    public Text TimerText;
    private void Update()
    {
        if(GameManager.Current_Game_Mode == Game_Mode.Time&!Time_Remaining.TimeOut()&standard_mode.Control_Panel.activeSelf)
        {
            Time_Remaining.Seconds -= Time.deltaTime;
            if (Time_Remaining.Seconds <= 0 & Time_Remaining.Minutes > 0)
            {
                Time_Remaining.Seconds = 60;
                Time_Remaining.Minutes--;
            }
            TimerText.text = $"{Time_Remaining.Minutes}:{Mathf.Round(Time_Remaining.Seconds)}";
            if (Time_Remaining.TimeOut())
            {
                standard_mode.Defeat();
            }
        }
       
    }
    private void OnDisable()
    {
        Time_Remaining = new Timer(0, 0);
        enabled = false;
    }
}
