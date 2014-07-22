using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BuildShopClickEvent()
	{
		GameStart.goBuildView.SetActive (true);
	}
}
