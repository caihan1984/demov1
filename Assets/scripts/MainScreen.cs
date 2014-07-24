using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainScreen : MonoBehaviour {

	//记录兵营招募图片索引
	public int iArmRecruitPicIndex = 0;

	//在进入招募部队界面时，需要记录此时全部可招募数量
	public static Dictionary<int, int> m_dArmCanRecruitNum = new Dictionary<int, int>();

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

	public void RefreshArmNumForRe()
	{
//		m_dArmCanRecruitNum.Add (cGameDataDef.PikeMan, CUser.Instance ().CampCanRecruitNum (cGameDataDef.PikeMan));
//		m_dArmCanRecruitNum.Add (cGameDataDef.Archer, CUser.Instance ().CampCanRecruitNum (cGameDataDef.Archer));
//		m_dArmCanRecruitNum.Add (cGameDataDef.Griffin, CUser.Instance ().CampCanRecruitNum (cGameDataDef.Griffin));
//		m_dArmCanRecruitNum.Add (cGameDataDef.SwordMan, CUser.Instance ().CampCanRecruitNum (cGameDataDef.SwordMan));
//		m_dArmCanRecruitNum.Add (cGameDataDef.Friar, CUser.Instance ().CampCanRecruitNum (cGameDataDef.Friar));
//		m_dArmCanRecruitNum.Add (cGameDataDef.Knight, CUser.Instance ().CampCanRecruitNum (cGameDataDef.Knight));
//		m_dArmCanRecruitNum.Add (cGameDataDef.Angel, CUser.Instance ().CampCanRecruitNum (cGameDataDef.Angel));

		m_dArmCanRecruitNum[cGameDataDef.PikeMan] = CUser.Instance ().CampCanRecruitNum (cGameDataDef.PikeMan);
		m_dArmCanRecruitNum[cGameDataDef.Archer] = CUser.Instance ().CampCanRecruitNum (cGameDataDef.Archer);
		m_dArmCanRecruitNum[cGameDataDef.Griffin] = CUser.Instance ().CampCanRecruitNum (cGameDataDef.Griffin);
		m_dArmCanRecruitNum[cGameDataDef.SwordMan] = CUser.Instance ().CampCanRecruitNum (cGameDataDef.SwordMan);
		m_dArmCanRecruitNum[cGameDataDef.Friar] = CUser.Instance ().CampCanRecruitNum (cGameDataDef.Friar);
		m_dArmCanRecruitNum[cGameDataDef.Knight] = CUser.Instance ().CampCanRecruitNum (cGameDataDef.Knight);
		m_dArmCanRecruitNum[cGameDataDef.Angel] = CUser.Instance ().CampCanRecruitNum (cGameDataDef.Angel);
	}

	public static int GetArmCanReNum(int iType)
	{
		return m_dArmCanRecruitNum [iType];
	}

	//兵营招兵
	public void BuildRecruitClickEvent(GameObject go)
	{
		GameStart.goRecruitArm.SetActive (true);

		RefreshArmNumForRe ();
		RecruitArm.iInitNum = 0;

		var Camp = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic");
		var Label = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic/CountLabel");
		var spCamp = Camp.GetComponent<UISprite> ();
		var lbLabel = Label.GetComponent<UILabel> ();
		if (go.name == "PikeMenCamp")
		{
			spCamp.spriteName = "1325.0";
			lbLabel.text = m_dArmCanRecruitNum[cGameDataDef.PikeMan].ToString();
			iArmRecruitPicIndex = 1;
		}
		else if (go.name == "ArcherCamp")
		{
			spCamp.spriteName = "1337.0";
			lbLabel.text = m_dArmCanRecruitNum[cGameDataDef.Archer].ToString();
			iArmRecruitPicIndex = 2;
		}
		else if (go.name == "GriffinCamp")
		{
			spCamp.spriteName = "1310.0";
			lbLabel.text = m_dArmCanRecruitNum[cGameDataDef.Griffin].ToString();
			iArmRecruitPicIndex = 3;
		}
		else if (go.name == "SwordCamp")
		{
			spCamp.spriteName = "1319.0";
			lbLabel.text = m_dArmCanRecruitNum[cGameDataDef.SwordMan].ToString();
			iArmRecruitPicIndex = 4;
		}
		else if (go.name == "FriarCamp")
		{
			spCamp.spriteName = "1343.0";
			lbLabel.text = m_dArmCanRecruitNum[cGameDataDef.Friar].ToString();
			iArmRecruitPicIndex = 5;
		}
		else if (go.name == "KnightCamp")
		{
			spCamp.spriteName = "1304.0";
			lbLabel.text = m_dArmCanRecruitNum[cGameDataDef.Knight].ToString();
			iArmRecruitPicIndex = 6;
		}
		else if (go.name == "ArcherCamp")
		{
			spCamp.spriteName = "1291.0";
			lbLabel.text = m_dArmCanRecruitNum[cGameDataDef.Angel].ToString();
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

		RefreshArmNumForRe ();

		var trPic = GameStart.goRecruitArm.transform.Find ("RecruitArmBG/SoldierPic");
		var spPic = trPic.GetComponent<UISprite> ();
		spPic.spriteName = m_dReArmPicIndex [iArmRecruitPicIndex];
		lbLabel.text =  m_dArmCanRecruitNum[iArmRecruitPicIndex].ToString();

		var spScroll = GameStart.goRecruitArm.transform.Find("RecruitArmBG/RecuScorll/");
		var scbScroll = spScroll.GetComponent<UIScrollBar> ();
		scbScroll.value = 0.0f;
		RecruitArm.iInitNum = 0;
	}
}
