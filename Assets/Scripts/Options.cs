using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public float textSpeed;
	public Button textSlow, textMid, textFast;

    public Canvas optionsCanvas;

    void Start()
    {
        optionsCanvas.enabled = false;
    }

    public void OpenOptions(bool state)
    {
        optionsCanvas.enabled = state;
    }

    public void OnUpdateOptions()
	{
		textSlow.GetComponent<Image>().color = textSlow.colors.normalColor;
		textMid.GetComponent<Image>().color = textMid.colors.normalColor;
        textFast.GetComponent<Image>().color = textFast.colors.normalColor;
		
		// UPDATE TEXT SPEED
		if (textSpeed < 40)         
			textSlow.GetComponent<Image>().color = textSlow.colors.highlightedColor;
		else if (textSpeed < 60)         
			textMid.GetComponent<Image>().color = textMid.colors.highlightedColor;
        else
            textFast.GetComponent<Image>().color = textFast.colors.highlightedColor;
			
	}
}
