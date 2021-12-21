using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
	public Image fill;


    public void setMaxStamina(float maxStamina)
	{
		slider.maxValue = maxStamina;
		slider.value = maxStamina;

        fill.color = gradient.Evaluate(1f);
	}

    public void setStamina(float stamina)
	{
		slider.value = stamina;

        fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
