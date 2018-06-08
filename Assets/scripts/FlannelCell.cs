using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlannelCell : MonoBehaviour {
	public Button button;
	public Text buttonText;

	/*  
		Flannel IDs are as follows:

		0: Green,
		1: Blue,
		2: Brown,
		3: Black,
		4: Pink.

		NOTE: If a possible ID is added here, a new flannel cell should be 
		added to the list.
	 */
	public int flannelID;

	/*
	    NOTE: If this is changed, the UI where the prices are listed should 
	    also be changed.

		Indexes correspond to flannel IDs.
	 */
	private static int[] flannelPrices = {
		0, 500, 1500, 4000, 10000,
	};

	private static List<FlannelCell> flannelCells = new List<FlannelCell>();

	private string playerPrefsOwnsKey;

	void Start() {
		playerPrefsOwnsKey = "owns flannel " + flannelID.ToString();

		// The first flannel should always be owned.
		if (flannelID == 0 && PlayerPrefs.GetInt(playerPrefsOwnsKey) != 1) {
			PlayerPrefs.SetInt(playerPrefsOwnsKey, 1);
		}

		flannelCells.Add(this);

		UpdateButton();
	}

	private void UpdateButton() {
		if (PlayerPrefs.GetInt(playerPrefsOwnsKey) == 1) {
			if (PlayerPrefs.GetInt("selected flannel") == flannelID) {
				button.interactable = false;
				buttonText.text = "Selected!";
			} else {
				button.interactable = true;
				buttonText.text = "Select";

				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(delegate {
					Select();
					UpdateAllButtons();
				});
			}
		} else {
			if (flannelPrices[flannelID] < PlayerPrefs.GetInt("coins")) {
				button.interactable = true;
				buttonText.text = "Buy!";

				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(delegate {
					Buy();
					UpdateAllButtons();
				});
			} else {
				button.interactable = false;
				buttonText.text = "Need More Coins";
			}
		}
	}

	private static void UpdateAllButtons() {
		for (int i = 0; i < flannelCells.Count; i++) {
			flannelCells[i].UpdateButton();
		}
	}

	private void Select() {
		PlayerPrefs.SetInt("selected flannel", flannelID);
	}

	private void Buy() {
		PlayerPrefs.SetInt(playerPrefsOwnsKey, 1);
		PlayerPrefs.SetInt(
			"coins", 
			PlayerPrefs.GetInt("coins") - flannelPrices[flannelID]
		);
		Select();
	}

	void OnDestroy() {
		button.onClick.RemoveAllListeners();
		flannelCells.Remove(this);
	}
}
