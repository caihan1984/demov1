using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainScreen : MonoBehaviour {

	//记录兵营招募图片索引
	public int iArmRecruitPicIndex = 0;

	//兵营招募图片索引
	public Dictionary<int, string>	m_dReArmPicIndex = new Dictionary<int, string> ();
	// Use this for initialization
	void Start () {
		m_dReArmPicIndex.Add (1, "1325.0");
		m_dReArmPicIndex.Add (2, "1337.0");
		m_dReArmPicIndex.Add (3, "1310.0");
		m_dReArmPicIndex.Add (4, "1319.0");
		m_dReArmPicIndex.Add (5, "1343.0");
		m_dReArmPicIndex.Add (6, "1304.0");
		m_dReArmPicIndex.Add (7, "1291.0");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BuildShopClickEvent()
	{
		GameStart.goBuildView.SetActive (true);
	}

	public void RecruitArmCancel()
	{
		GameStart.goRecruitArm.SetActive (false);
	}

	//兵营招兵
	public void BuildRecruitClickEvent(GameObject go)
	{
		GameStart.goRecruitArm.SetActive (true);

		var Camp = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic");
		var Label = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic/CountLabel");
		var spCamp = Camp.GetComponent<UISprite> ();
		var lbLabel = Label.GetComponent<UILabel> ();
		if (go.name == "PikeMenCamp")
		{
			spCamp.spriteName = "1325.0";
			lbLabel.text = CUser.Instance().CampCanRecruitNum(cGameDataDef.PikeMan).ToString();
			iArmRecruitPicIndex = 1;
		}
		else if (go.name == "ArcherCamp")
		{
			spCamp.spriteName = "1337.0";
			lbLabel.text = CUser.Instance().CampCanRecruitNum(cGameDataDef.Archer).ToString();
			iArmRecruitPicIndex = 2;
		}
		else if (go.name == "GriffinCamp")
		{
			spCamp.spriteName = "1310.0";
			lbLabel.text = CUser.Instance().CampCanRecruitNum(cGameDataDef.Griffin).ToString();
			iArmRecruitPicIndex = 3;
		}
		else if (go.name == "SwordCamp")
		{
			spCamp.spriteName = "1319.0";
			lbLabel.text = CUser.Instance().CampCanRecruitNum(cGameDataDef.SwordMan).ToString();
			iArmRecruitPicIndex = 4;
		}
		else if (go.name == "FriarCamp")
		{
			spCamp.spriteName = "1343.0";
			lbLabel.text = CUser.Instance().CampCanRecruitNum(cGameDataDef.Friar).ToString();
			iArmRecruitPicIndex = 5;
		}
		else if (go.name == "KnightCamp")
		{
			spCamp.spriteName = "1304.0";
			lbLabel.text = CUser.Instance().CampCanRecruitNum(cGameDataDef.Knight).ToString();
			iArmRecruitPicIndex = 6;
		}
		else if (go.name == "ArcherCamp")
		{
			spCamp.spriteName = "1291.0";
			lbLabel.text = CUser.Instance().CampCanRecruitNum(cGameDataDef.Angel).ToString();
			iArmRecruitPicIndex = 7;
		}
	}

	//切换招募兵种
	public void RecruitArmSwitchClickEvent(GameObject go)
	{
		var Label = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic/CountLabel");
		var lbLabel = Label.GetComponent<UILabel> ();
		if (go.name == "Left")
		{
			iArmRecruitPicIndex -= 1;
			if (iArmRecruitPicIndex <= 0) iArmRecruitPicIndex = 7;
		}
		else if (go.name == "Right")
		{
			iArmRecruitPicIndex += 1;
			if (iArmRecruitPicIndex >= 8) iArmRecruitPicIndex = 1;
		}

		var trPic = GameStart.goRecruitArm.transform.Find ("RecruitArmBG/SoldierPic");
		var spPic = trPic.GetComponent<UISprite> ();
		spPic.spriteName = m_dReArmPicIndex [iArmRecruitPicIndex];
		lbLabel.text = CUser.Instance().CampCanRecruitNum(iArmRecruitPicIndex).ToString();
	}
}
