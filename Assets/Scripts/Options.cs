using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public float textSpeed;
	public Button textSlow, textMid, textFast;

	public float walkSpeed;
	public Button walkSlow, walkMid, walkFast;

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
        // UPDATE TEXT SPEED
		textSlow.GetComponent<Image>().color = textSlow.colors.normalColor;
		textMid.GetComponent<Image>().color = textMid.colors.normalColor;
        textFast.GetComponent<Image>().color = textFast.colors.normalColor;

		if (textSpeed < 40)         
			textSlow.GetComponent<Image>().color = textSlow.colors.highlightedColor;
		else if (textSpeed < 60)         
			textMid.GetComponent<Image>().color = textMid.colors.highlightedColor;
        else
            textFast.GetComponent<Image>().color = textFast.colors.highlightedColor;

        // UPDATE WALK SPEED
        walkSlow.GetComponent<Image>().color = walkSlow.colors.normalColor;
        walkMid.GetComponent<Image>().color = walkMid.colors.normalColor;
        walkFast.GetComponent<Image>().color = walkFast.colors.normalColor;

        if (walkSpeed < 4)
            walkSlow.GetComponent<Image>().color = walkSlow.colors.highlightedColor;
        else if (walkSpeed < 6)
            walkMid.GetComponent<Image>().color = walkMid.colors.highlightedColor;
        else
            walkFast.GetComponent<Image>().color = walkFast.colors.highlightedColor;
	}
}
