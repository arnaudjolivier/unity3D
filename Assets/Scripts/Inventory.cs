using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public static Inventory instance; //permet d acceder au script n importe ou
    public Text coinsCountText;

    private void Awake()
    {
        if(instance != null) 
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scéne");
            return;
        }
        instance = this;
    }
    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }
    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        coinsCountText.text = coinsCount.ToString();
    }
}
