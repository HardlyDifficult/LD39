using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RngFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
    GetComponent<TextMesh>().color = UnityEngine.Random.ColorHSV();
    transform.localScale *= UnityEngine.Random.Range(.5f, 2f);
    transform.position = new Vector3(UnityEngine.Random.Range(-18, 18), transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
