using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Storage : MonoBehaviour {
	public static void SetNumber(string key, long value) {
		PlayerPrefs.SetString(key, value.ToString());
	}

	private static long GetNumberFromString(string key) {
		string stringValue = PlayerPrefs.GetString(key);

		try {
			return long.Parse(stringValue);
		} catch (Exception ex) {
			return 0;
		}
	}

	public static long GetNumber(string key) {
		/*
			First, check if there is a holdover `int` value from before strings 
			were used.
		 */
		long oldIntValue = PlayerPrefs.GetInt(key);
		long numberFromString = GetNumberFromString(key);

		if (oldIntValue != 0 && numberFromString == 0) {
			PlayerPrefs.SetInt(key, 0);
			SetNumber(key, oldIntValue);
			return oldIntValue;
		} else {
			return numberFromString;
		};
	}
}
