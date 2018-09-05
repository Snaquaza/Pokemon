using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public Canvas menuCanvas;
	public Canvas inventoryCanvas;
	public Canvas optionsCanvas;
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
		transform.parent.parent.parent.GetComponent<MenuUI>().setCurrentButton(button);
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
        FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>().isInventory = true;
		FindObjectOfType<InventoryHandler>().GetComponentInChildren<ShowItems>().OnOpenBag();
	}
    public void OpenOptions()
	{
		SelectButton();
		optionsCanvas.enabled = true;  
        menuCanvas.enabled = false;  
		FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>().isMenu = false;
		FindObjectOfType<PlayerControl>().gameObject.GetComponent<PlayerControl>().isOptions = true;
        FindObjectOfType<Options>().OnUpdateOptions();   
	}
}
