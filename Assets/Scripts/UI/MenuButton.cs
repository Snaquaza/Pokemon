using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {
	
    public Canvas canvas;
	public Button button;

	public void Highlight(bool state)
	{
		if (state)
			button.GetComponent<Image>().color = button.colors.highlightedColor;
		else
			button.GetComponent<Image>().color = button.colors.normalColor;
	}

    public void SelectButton()
	{
		transform.parent.parent.parent.GetComponent<InventoryUI>().setCurrentButton(gameObject);
	}

    public void CloseMenu()
    {
        canvas.enabled = false;
        FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>().isInventory = false;
    }
}
