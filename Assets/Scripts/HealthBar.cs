using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health) // permet de set la barre de vie a 100 au depart
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f); // change la couleurs de la barre de vie
    }
    public void SetHealth(int health) // indiquer quelle valeur a la barre de vie
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
