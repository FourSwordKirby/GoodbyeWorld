using UnityEngine;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	
	private List<GameObject> objects;
	
	// Use this for initialization
	void Start () {
		objects = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void CreateObject() {
	}
	
	public void DestroyObject() {
	}
	
	public void ChangeSelection(bool next) {
	}
	
	public void TurnSun(float deg) {
	}
}

