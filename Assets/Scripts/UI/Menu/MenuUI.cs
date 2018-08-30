using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	public Canvas menuCanvas;
    
	public Button currentButton;
	public Button[] buttons;
	int buttonIndex;

	// Use this for initialization
	void Start () {
        menuCanvas.enabled = false;
		buttonIndex = 0;
		currentButton = buttons[buttonIndex];
	}

	public void OpenInventory(bool state)
    {
        menuCanvas.enabled = state;
		currentButton.GetComponent<MenuButton>().Highlight(true);
    }

	public void Up()
	{
		buttonIndex++;
        UpdateButton();
	}

	public void Down()
	{
		buttonIndex--;
        UpdateButton();
	}

	public void setCurrentButton(GameObject button)   
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			if(buttons[i] == button)
			{
				buttonIndex = i;
                UpdateButton();
			}
		}
	}

    private void UpdateButton()
	{
        currentButton.GetComponent<MenuButton>().Highlight(false);
		buttonIndex = (buttonIndex + buttons.Length) % buttons.Length;
		currentButton = buttons[buttonIndex];
		currentButton.GetComponent<MenuButton>().Highlight(true);
	}
    
    public void clickButton()
	{
		currentButton.onClick.Invoke();
	}
}
