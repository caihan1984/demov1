using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//遇到的问题
//scroll bar 的滑动杆怎么随数字移动

//定义一些全局的数据变量
public class cGameDataDef
{
	public static int WatchTower = 1;
	public static int ShooterTower = 2;
	public static int GriffinTower = 3;
	public static int CampTower = 4;
	public static int Temple = 5;
	public static int TrainGround = 6;
	public static int Heaven = 7;
	
	public static int BuildingType = 7;			//建筑种类
	
	public static int ArmOnBattleNum = 7;		//出征部队最大类型数
	
	public static int PikeMan = 1;				//步兵
	public static int Archer = 2;				//弓箭
	public static int Griffin = 3;				//狮鹫骑士
	public static int SwordMan = 4;				//剑士
	public static int Friar = 5;				//修道士
	public static int Knight = 6;				//骑士
	public static int Angel = 7;				//天使

	public static int PikeManCD = 1;				//步兵
	public static int ArcherCD = 2;				//弓箭
	public static int GriffinCD = 3;				//狮鹫骑士
	public static int SwordManCD = 4;				//剑士
	public static int FriarCD = 5;				//修道士
	public static int KnightCD = 6;				//骑士
	public static int AngelCD = 7;				//天使
}

//玩家数据
public class CUser
{
	//	public int[] UserBuilds = new int[cGameDataDef.BuildingType];
	//建筑信息
	public Dictionary<int, int>	m_dUserBuilds = new Dictionary<int, int> ();

	//玩家部队
	public Arm[] m_armInfo = new Arm[cGameDataDef.ArmOnBattleNum];

	//兵营可招募部队
	public Dictionary<int, int> dArmCanRecruit = new Dictionary<int, int>();

	private static CUser m_instance = null;

	//部队信息	
	public struct Arm
	{
		public int iType;	//类型
		public int iNum;	//数量
		public int iStar;	//星级
	}
	
	public CUser()
	{
		m_dUserBuilds.Add (cGameDataDef.WatchTower, 0);
		m_dUserBuilds.Add (cGameDataDef.ShooterTower, 0);
		m_dUserBuilds.Add (cGameDataDef.GriffinTower, 0);
		m_dUserBuilds.Add (cGameDataDef.CampTower, 0);
		m_dUserBuilds.Add (cGameDataDef.Temple, 0);
		m_dUserBuilds.Add (cGameDataDef.TrainGround, 0);
		m_dUserBuilds.Add (cGameDataDef.Heaven, 0);

		dArmCanRecruit.Add (cGameDataDef.WatchTower, 0);
		dArmCanRecruit.Add (cGameDataDef.ShooterTower, 0);
		dArmCanRecruit.Add (cGameDataDef.GriffinTower, 0);
		dArmCanRecruit.Add (cGameDataDef.CampTower, 0);
		dArmCanRecruit.Add (cGameDataDef.Temple, 0);
		dArmCanRecruit.Add (cGameDataDef.TrainGround, 0);
		dArmCanRecruit.Add (cGameDataDef.Heaven, 0);

		for (int i = 0; i < cGameDataDef.ArmOnBattleNum; i++)
		{
			m_armInfo[i].iNum = 0;
			m_armInfo[i].iStar = 0;
			m_armInfo[i].iType = 0;
		}
	}
	
	public static CUser Instance()
	{
		if (m_instance == null) 
		{
			m_instance = new CUser ();
		}
		return m_instance;
	}
	
	public int GetBuildLevel(int iBType)
	{
		return m_dUserBuilds [iBType];
	}

	//兵营生产
	public void CampProduct(int iType)
	{
		dArmCanRecruit [iType] += 1;
	}

	//兵营可招募部队
	public int CampCanRecruitNum(int iType)
	{
		return dArmCanRecruit [iType];
	}

	public static long GetSysTime()
	{
		return ((System.DateTime.Now.ToUniversalTime ().Ticks - 621355968000000000) / 10000000);
	}
	
	//public void RecruitArm(int iArmType, int iNum, int iStar)
	public void RecruitArm(Arm armInfo)
	{
		Debug.Log("recruit arm");
		for (int i = 0; i < cGameDataDef.ArmOnBattleNum; ++i)
		{
			if (m_armInfo[i].iType == 0)
			{
				m_armInfo[i] = armInfo;
				return;
			}
		}
		return;
	}
}

//资源数据
public class CGameResource
{

}

public class GameStart : MonoBehaviour {
//	private static GameStart m_instance = null;

	public static GameObject goMainScreen;
	public static GameObject goBuildView;
	public static GameObject goRecruitArm;

	//部队生产时间
	public  long PikeManProTime;				//步兵
	public  long ArcherProTime;				//弓箭
	public  long GriffinProTime;				//狮鹫骑士
	public  long SwordManProTime;				//剑士
	public  long FriarProTime;				//修道士
	public  long KnightProTime;				//骑士
	public  long AngelProTime;				//天使
	// Use this for initialization
	void Awake()
	{
		goMainScreen = GameObject.Find ("MainScreenPanel");
		goBuildView = GameObject.Find ("BuildingViewPanel");
		goRecruitArm = GameObject.Find ("RecruitArmPanel");

		goBuildView.SetActive (false);
		goRecruitArm.SetActive (false);
	}
	void Start () {
//		goMainScreen = GameObject.Find ("MainScreenPanel");
//		goBuildView = GameObject.Find ("BuildingViewPanel");
//		GameObject.Find ("BuildingViewPanel").SetActive (false);
		PikeManProTime = CUser.GetSysTime ();
		ArcherProTime = CUser.GetSysTime ();
		GriffinProTime = CUser.GetSysTime ();
		SwordManProTime = CUser.GetSysTime ();
		FriarProTime = CUser.GetSysTime ();
		KnightProTime = CUser.GetSysTime ();
		AngelProTime = CUser.GetSysTime ();
	}


	
	// Update is called once per frame
	void Update () {
		long iTime = CUser.GetSysTime ();
		if (iTime - PikeManProTime >= cGameDataDef.PikeManCD)
		{
			CUser.Instance().CampProduct(cGameDataDef.PikeMan);
			PikeManProTime = iTime;
		}
		if (iTime - ArcherProTime >= cGameDataDef.ArcherCD)
		{
			CUser.Instance().CampProduct(cGameDataDef.Archer);
			ArcherProTime = iTime;
		}
		if (iTime - GriffinProTime >= cGameDataDef.GriffinCD)
		{
			CUser.Instance().CampProduct(cGameDataDef.Griffin);
			GriffinProTime = iTime;
		}
		if (iTime - SwordManProTime >= cGameDataDef.SwordManCD)
		{
			CUser.Instance().CampProduct(cGameDataDef.SwordMan);
			SwordManProTime = iTime;
		}
		if (iTime - FriarProTime >= cGameDataDef.FriarCD)
		{
			CUser.Instance().CampProduct(cGameDataDef.Friar);
			FriarProTime = iTime;
		}
		if (iTime - KnightProTime >= cGameDataDef.KnightCD)
		{
			CUser.Instance().CampProduct(cGameDataDef.Knight);
			KnightProTime = iTime;
		}
		if (iTime - AngelProTime >= cGameDataDef.AngelCD)
		{
			CUser.Instance().CampProduct(cGameDataDef.Angel);
			AngelProTime = iTime;
		}
	}

//	public static GameStart Instance()
//	{
//		if (m_instance == null) 
//		{
//			m_instance = new GameStart ();
//		}
//		return m_instance;
//	}
}
