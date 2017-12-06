using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTiming : MonoBehaviour
{
	public SpriteRenderer s1;
	public SpriteRenderer s2;
	public SpriteRenderer s3;

	public AnimationCurve ac1;
	public AnimationCurve ac2;
	public AnimationCurve ac3;
	private float _start;

	// Use this for initialization
	void Start ()
	{
		_start = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{

		var c = s1.color;
		c.a = ac1.Evaluate(Time.time - _start);
		s1.color = c;
		
		c = s2.color;
		c.a = ac2.Evaluate(Time.time - _start);
		s2.color = c;
		
		c = s3.color;
		c.a = ac3.Evaluate(Time.time - _start);
		s3.color = c;

	}
}
