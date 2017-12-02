using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Floating {

	
	// Update is called once per frame
	void Update () {
		base.Update();
		transform.position += FloatDirection * Time.deltaTime * FloatSpeed;
	}
}
