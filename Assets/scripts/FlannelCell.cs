using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlannelCell : MonoBehaviour {
	public Button button;
	public Text buttonText;
	public CoinsStatus coinsStatus;

	/*  
		Flannel IDs are as follows:

		0: Green,
		1: Blue,
		2: Brown,
		3: Black,
		4: Pink,
		5: Gold.

		NOTE: If a possible ID is added here, a new flannel cell should be 
		added to the list and to flannel sprites for `FlannelManager`.
	 */
	public int flannelID;

	/*
	    NOTE: If this is changed, the UI where the prices are listed should 
	    also be changed.

		Indexes correspond to flannel IDs.
	 */
	private static long[] flannelPrices = {
		0, 1000, 3000, 8000, 13000, 26000
	};

	private static List<FlannelCell> flannelCells = new List<FlannelCell>();

	private string playerPrefsOwnsKey;

	void Start() {
		playerPrefsOwnsKey = "owns flannel " + flannelID.ToString();

		// The first flannel should always be owned.
		if (flannelID == 0 && Storage.GetNumber(playerPrefsOwnsKey) != 1) {
			Storage.SetNumber(playerPrefsOwnsKey, 1);
		}

		flannelCells.Add(this);

		UpdateButton();
	}

	private void UpdateButton() {
		if (Storage.GetNumber(playerPrefsOwnsKey) == 1) {
			if (Storage.GetNumber("selected flannel") == flannelID) {
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
			if (flannelPrices[flannelID] <= Storage.GetNumber("coins")) {
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
		Storage.SetNumber("selected flannel", flannelID);
	}

	private void Buy() {
		long newAmount = Storage.GetNumber("coins") - flannelPrices[flannelID];

		Storage.SetNumber(playerPrefsOwnsKey, 1);
		Storage.SetNumber("coins", newAmount);
		coinsStatus.UpdateCoinsAmount(newAmount);
		Select();
	}

	void OnDestroy() {
		button.onClick.RemoveAllListeners();
		flannelCells.Remove(this);
	}
}
