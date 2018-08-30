using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public Canvas menuCanvas;
	public Canvas inventoryCanvas;
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
		Debug.Log("Highlighting " + gameObject);
		transform.parent.parent.parent.GetComponent<InventoryUI>().setCurrentButton(gameObject);
	}

    public void CloseMenu()
    {
		SelectButton();
        menuCanvas.enabled = false;
        FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>().isMenu = false;
    }

    public void OpenInventory()
	{
		SelectButton();
		inventoryCanvas.enabled = true;    
        FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>().isMenu = false;  
	}
}
