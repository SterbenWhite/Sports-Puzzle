using UnityEngine;

public class Shop : MonoBehaviour
{
    public Standard_Mode standard_Mode;
 
    public void BuyTips(Purchase purchase)
    {
        if(GameManager.Stats.Money >= purchase.Price)
        {
            standard_Mode.Manager.AddTips(purchase.Amount);
            standard_Mode.Manager.AddMoney(-purchase.Price);
            Debug.Log($"Buy: {purchase.Amount} Tips");
        }
    }
    public void BuyLifes(Purchase purchase)
    {
        if (GameManager.Stats.Money >= purchase.Price)
        {
            standard_Mode.Manager.AddLifes(purchase.Amount);
            standard_Mode.Manager.AddMoney(-purchase.Price);
            Debug.Log($"Buy: {purchase.Amount} Lifes");
        }
    }
}
