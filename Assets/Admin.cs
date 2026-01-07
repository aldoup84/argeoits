using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string id = PlayerPrefs.GetString("id");
		string nombre = PlayerPrefs.GetString("nombre");
		int derechos = PlayerPrefs.GetInt("derechos");
		// Debug.Log("Id: " + id + "		" + derechos);
		if (id.Equals("0") && derechos == 0 || nombre.Equals("Admin") && derechos == 0)
		{
			GameObject.Find("btnPanelOpciones").SetActive(true);
			GameObject.Find("btnAcercaDe").SetActive(false);
		}
		else {
			GameObject.Find("btnPanelOpciones").SetActive(false);
			GameObject.Find("btnAcercaDe").SetActive(true);
		}

	}
 
}

