using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
}

//玩家数据
public class CUser
{
	//	public int[] UserBuilds = new int[cGameDataDef.BuildingType];
	//建筑信息
	public Dictionary<int, int>	m_dUserBuilds = new Dictionary<int, int> ();
	
	//部队信息	
	public struct Arm
	{
		public int iType;	//类型
		public int iNum;	//数量
		public int iStar;	//星级
	}
	
	//玩家部队
	public Arm[] m_armInfo = new Arm[cGameDataDef.ArmOnBattleNum];
	
	private static CUser m_instance = null;
	
	public CUser()
	{
		m_dUserBuilds.Add (cGameDataDef.WatchTower, 1);
		m_dUserBuilds.Add (cGameDataDef.ShooterTower, 1);
		m_dUserBuilds.Add (cGameDataDef.GriffinTower, 0);
		m_dUserBuilds.Add (cGameDataDef.CampTower, 0);
		m_dUserBuilds.Add (cGameDataDef.Temple, 0);
		m_dUserBuilds.Add (cGameDataDef.TrainGround, 0);
		m_dUserBuilds.Add (cGameDataDef.Heaven, 0);
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
	// Use this for initialization
	void Awake()
	{
		goMainScreen = GameObject.Find ("MainScreenPanel");
		goBuildView = GameObject.Find ("BuildingViewPanel");
		goBuildView.SetActive (false);
	}
	void Start () {
//		goMainScreen = GameObject.Find ("MainScreenPanel");
//		goBuildView = GameObject.Find ("BuildingViewPanel");
//		GameObject.Find ("BuildingViewPanel").SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
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
