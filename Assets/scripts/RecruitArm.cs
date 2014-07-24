using UnityEngine;
using System.Collections;

public class RecruitArm : MonoBehaviour {
	public static int iInitNum;
	// Use this for initialization
	void Start () {
		iInitNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RecruitArmClickEvent(GameObject go)
	{
		//可招募数量
		var sLabel = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic/CountLabel");
		var lbsLabel = sLabel.GetComponent<UILabel> ();

		//已经选择数量
		var rLabel = GameStart.goRecruitArm.transform.Find("RecruitArmBG/RecuScorll/CountLabel");
		var lbrLabel = rLabel.GetComponent<UILabel> ();

		//scrollbar
		var spScroll = GameStart.goRecruitArm.transform.Find("RecruitArmBG/RecuScorll/");
		var scbScroll = spScroll.GetComponent<UIScrollBar> ();

		//看是哪个兵种界面，获取对应可招募兵种数量
		var trArmType = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic/");
		var spArmType = trArmType.GetComponent<UISprite> ();

		int iTotalNum = 0;
		if (spArmType.spriteName == "1325.0")
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.PikeMan];
		}
		else if (spArmType.spriteName == "1337.0")
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Archer];
		}
		else if (spArmType.spriteName == "1310.0")
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Griffin];
		}
		else if (spArmType.spriteName == "1319.0")
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.SwordMan];
		}
		else if (spArmType.spriteName == "1343.0")
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Friar];
		}
		else if (spArmType.spriteName == "1304.0")
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Knight];
		}
		else if (spArmType.spriteName == "1291.0")
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Angel];
		}

		if (go.name == "AddSprite")
		{
			int iLeftNum = int.Parse(lbsLabel.text);
			if (iLeftNum <= 0) return;
			iInitNum += 1;

			lbsLabel.text = (iLeftNum - 1).ToString();

			scbScroll.value = (float)iInitNum / (float)iTotalNum;

			Debug.Log("f " + scbScroll.value);

			float a = scbScroll.value * (float)iTotalNum;
	
		//	int b = (int)a;
			lbrLabel.text = ((int)a).ToString();
		}
	}
}
