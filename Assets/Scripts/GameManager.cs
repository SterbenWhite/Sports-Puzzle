using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public static Game_Mode Current_Game_Mode;
    public static Drag_Puzzle Dragged;
    public static GameStats Stats;
    public Text MoneyText;
    public Text LifesText;
    public Text TipsText;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Stats = new GameStats();
        Dragged = FindObjectOfType<Drag_Puzzle>();
        MoneyText.text = Stats.Money.ToString();
        LifesText.text = $"Lifes: {Stats.Lifes}";
        TipsText.text = $"Tips: {Stats.Tips}";
    }
    public void AddMoney(int amount)
    {
        Stats.Money += amount;
        MoneyText.text = Stats.Money.ToString();
    }
    public void AddLifes(int amount)
    {
        Stats.Lifes += amount;
        LifesText.text = $"Lifes: {Stats.Lifes}";
    }
    public void AddTips(int amount)
    {
        Stats.Tips += amount;
        TipsText.text = $"Tips: {Stats.Tips}";
    }
    public void SetGameMode(int mode)
    {
        Current_Game_Mode = (Game_Mode)mode;
        Debug.Log($"Selected {(Game_Mode)mode} mode");
    }
}
[System.Serializable]
public enum Game_Mode
{
    Standard,
    Time,
    Custom
}
[System.Serializable]
public class GameStats
{
    public int Money;
    public int Lifes;
    public int Tips;
    public GameStats()
    {
        Money = 250;
        Lifes = 5;
        Tips = 2;
    }
}
