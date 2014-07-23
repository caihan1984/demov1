using UnityEngine;
using System.Collections;

public class RecruitArm : MonoBehaviour {
	int iInitNum;
	// Use this for initialization
	void Start () {
		iInitNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RecruitArmClickEvent(GameObject go)
	{
		var sLabel = GameStart.goRecruitArm.transform.Find("RecruitArmBG/SoldierPic/CountLabel");
		var lbsLabel = sLabel.GetComponent<UILabel> ();

		var rLabel = GameStart.goRecruitArm.transform.Find("RecruitArmBG/RecuScorll/CountLabel");
		var lbrLabel = rLabel.GetComponent<UILabel> ();
		if (go.name == "AddSprite")
		{
			iInitNum += 1;
			int iLeftNum = int.Parse(lbsLabel.text);
			lbsLabel.text = (iLeftNum - 1).ToString();
			lbrLabel.text = 0.33.ToString();
		}
	}
}
