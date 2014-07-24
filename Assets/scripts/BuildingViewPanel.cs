using UnityEngine;
using System.Collections;
using System.Timers;

public class BuildingViewPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BuildViewCancelClickEvent()
	{
		GameStart.goBuildView.SetActive (false);
	}

	public void BuildingOnBuildClickEvent(GameObject go)
	{
//		Debug.Log ("aa" + go.name + " " +  float.Parse(1 / 11));
		GameStart.goBuildView.SetActive (false);
		MainScreen.OffOnShopButton (true);
	
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
			CUser.Instance().CampBuildUp(cGameDataDef.PikeMan);
		}
		else if (go.name == "ArcherBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/ArcherCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
			CUser.Instance().CampBuildUp(cGameDataDef.Archer);
		}
		else if (go.name == "GriffinBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/GriffinCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
			CUser.Instance().CampBuildUp(cGameDataDef.Griffin);
		}
		else if (go.name == "SwordManBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/SwordCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
			CUser.Instance().CampBuildUp(cGameDataDef.SwordMan);
		}
		else if (go.name == "FriarBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/FriarCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
			CUser.Instance().CampBuildUp(cGameDataDef.Friar);
		}
		else if (go.name == "KnightBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/KnightCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
			CUser.Instance().CampBuildUp(cGameDataDef.Knight);
		}
		else if (go.name == "AngelBuild")
		{
			var goCamp = GameStart.goMainScreen.transform.Find("mainBGR/AngelCamp");
			goCamp.gameObject.SetActive(true);
			UIPlayTween upt = goCamp.GetComponent<UIPlayTween>();
			upt.Play(true);
			CUser.Instance().CampBuildUp(cGameDataDef.Angel);
		}
	}
}
