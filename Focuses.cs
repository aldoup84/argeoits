using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Focuses : MonoBehaviour
{

	InputField input1;    //ID
	InputField AddUser;
	InputField AddName;
	InputField addPass;
	InputField AddEscuela;
	InputField AddGrupo;
	InputField AddStatus;

	public void NameFocus() {
		GameObject.Find("InputNombre").GetComponent<InputField>().ActivateInputField();
	}

}
