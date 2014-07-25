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
		int iType = 0;
		if (spArmType.spriteName == CUser.Instance().dArmPic[cGameDataDef.PikeMan])
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.PikeMan];
			iType = cGameDataDef.PikeMan;
		}
		else if (spArmType.spriteName == CUser.Instance().dArmPic[cGameDataDef.Archer])
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Archer];
			iType = cGameDataDef.Archer;
		}
		else if (spArmType.spriteName == CUser.Instance().dArmPic[cGameDataDef.Griffin])
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Griffin];
			iType = cGameDataDef.Griffin;
		}
		else if (spArmType.spriteName == CUser.Instance().dArmPic[cGameDataDef.SwordMan])
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.SwordMan];
			iType = cGameDataDef.SwordMan;
		}
		else if (spArmType.spriteName == CUser.Instance().dArmPic[cGameDataDef.Friar])
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Friar];
			iType = cGameDataDef.Friar;
		}
		else if (spArmType.spriteName == CUser.Instance().dArmPic[cGameDataDef.Knight])
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Knight];
			iType = cGameDataDef.Knight;
		}
		else if (spArmType.spriteName == CUser.Instance().dArmPic[cGameDataDef.Angel])
		{
			iTotalNum = MainScreen.m_dArmCanRecruitNum[cGameDataDef.Angel];
			iType = cGameDataDef.Angel;
		}

		int iLeftNum = 0;
		if (go.name == "AddSprite")
		{
			iLeftNum = int.Parse(lbsLabel.text);
			if (iLeftNum <= 0) return;
			iInitNum += 1;
			lbsLabel.text = (iLeftNum - 1).ToString();
			scbScroll.value = (float)iInitNum / (float)iTotalNum;
			lbrLabel.text = ((int)(scbScroll.value * (float)iTotalNum)).ToString();
		}
		else if (go.name == "ReduceSprite")
		{
			iLeftNum = int.Parse(lbsLabel.text);
			if (iLeftNum >= iTotalNum) return;
			iInitNum -= 1;
			lbsLabel.text = (iLeftNum + 1).ToString();
			scbScroll.value = (float)iInitNum / (float)iTotalNum;
			lbrLabel.text = ((int)(scbScroll.value * (float)iTotalNum)).ToString();
		}
		else if (go.name == "ConfirmSprite")
		{
			Arm armInfo = new Arm();
			armInfo.Init(iType, iInitNum, 1);

			GameStart.goArmStore.SetActive(true);
			var gridArm = GameStart.goArmStore.transform.Find("Grid");

			for (int i = 0; i < cGameDataDef.ArmOnBattleNum; ++i)
			{
				var child = gridArm.GetChild(i);
				if (CUser.Instance().m_armInfo[i].iNum > 0)
				{
					var cs = child.transform.Find("Sprite");
					var sp = cs.GetComponent<UISprite>();
					sp.spriteName = CUser.Instance().dArmPic[CUser.Instance().m_armInfo[i].iType];

					var cl = child.transform.Find("Label");
					var lb = cl.GetComponent<UILabel>();

					lb.text = (CUser.Instance().m_armInfo[i].iNum).ToString();
				}
			}
		}
	}
}
