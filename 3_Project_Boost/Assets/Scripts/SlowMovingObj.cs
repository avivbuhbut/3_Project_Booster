﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovingObj : MonoBehaviour
{


	public Vector3 pointB;

	IEnumerator Start()
	{
		var pointA = transform.position;
		while (true)
		{
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 2.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 2.0f));
		}
	}

	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 5.0f / time;
		while (i < rate)
		{
            i += Time.deltaTime; //* rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}
}
