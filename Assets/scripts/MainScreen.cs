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

	//兵营招兵
	public void BuildRecruitClickEvent(GameObject go)
	{
		GameStart.goRecruitArm.SetActive (true);

		var Camp = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic");
		var Label = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic/Label");
		var spCamp = Camp.GetComponent<UISprite> ();
		var lbLabel = Label.GetComponent<UILabel> ();
		if (go.name == "PikeMenCamp")
		{
			spCamp.spriteName = "1325.0";
			lbLabel.text = "123";
		}
		else if (go.name == "ArcherCamp")
		{
			spCamp.spriteName = "1337.0";
			lbLabel.text = "123";
		}
		else if (go.name == "GriffinCamp")
		{
			spCamp.spriteName = "1310.0";
			lbLabel.text = "123";
		}
		else if (go.name == "SwordCamp")
		{
			spCamp.spriteName = "1319.0";
			lbLabel.text = "123";
		}
		else if (go.name == "FriarCamp")
		{
			spCamp.spriteName = "1343.0";
			lbLabel.text = "123";
		}
		else if (go.name == "KnightCamp")
		{
			spCamp.spriteName = "1304.0";
			lbLabel.text = "123";
		}
		else if (go.name == "ArcherCamp")
		{
			spCamp.spriteName = "1291.0";
			lbLabel.text = "123";
		}
	}
}
