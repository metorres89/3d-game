using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DroppableItem : System.Object
{
	public GameObject itemPrototype;
	[Range(0.0f, 1.0f)] public float chanceToDrop;
}
