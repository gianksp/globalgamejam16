﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Cube : NetworkItem {

	private Vector3 _targetPosition;
	private Quaternion _targetRotation;

	/// <summary>
	/// Fixeds the update. Only the player controlling can write to firebase, everyone else must read and interpret
	/// </summary>
	public void Update() {
		base.Update ();
		//Non playable draw based on properties read
		InterpretValues ();
	}

	/// <summary>
	/// Based on the values stored in "SetValues" use them to update this gameobject instance
	/// </summary>
	public void InterpretValues() {
		try {
			if (_properties != null && _properties.Count > 0) {
				//Update position
				_targetPosition    = JsonUtility.FromJson<Vector3> (_properties["position"].ToString());
				transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime*5f);

				_targetRotation    = JsonUtility.FromJson<Quaternion> (_properties["rotation"].ToString());
				transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime*5f);
			}
		} catch (Exception ex) { }
	}
}