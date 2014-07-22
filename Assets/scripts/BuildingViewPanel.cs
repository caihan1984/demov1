using UnityEngine;
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
	}
}
