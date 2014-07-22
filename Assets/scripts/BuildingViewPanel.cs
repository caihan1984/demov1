﻿using UnityEngine;
using System.Collections;

public class BuildingViewPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BuildingOnBuildClickEvent(GameObject go)
	{
		Debug.Log ("aa" + go.name);
		GameStart.goBuildView.SetActive (false);
	
//		Transform goCamp;
		if (go.name == "PikeManBuild")
		{
			//为什么GameStart.goMainScreen没有find
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/PikeMenCamp");
			goCamp.gameObject.SetActive(true);
		//	goCamp.GetComponent<gameObject>().SetActive(true);
		//	goCamp.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
		}
		else if (go.name == "ArcherBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/ArcherCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
		}
		else if (go.name == "GriffinBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/GriffinCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
		}
		else if (go.name == "SwordManBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/SwordCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
		}
		else if (go.name == "FriarBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/FriarCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
		}
		else if (go.name == "KnightBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/KnightCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
		}
		else if (go.name == "AngelBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/AngelCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
		}
	}
}
