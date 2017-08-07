using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (EnemyState))]
public class EnemyItemDrop : MonoBehaviour {

	[SerializeField] [Range(0.0f, 1.0f)]private float chanceToDropItem = 0.25f;
	public DroppableItem[] droppableItems;

	private float totalChance;
	private EnemyState myEnemyState;

	public void DropRandomItem() {
		float dropChance = Random.Range (0.0f, 1.0f);

		if (dropChance <= chanceToDropItem) {
			float itemChance = Random.Range (0.0f, totalChance);

			for (int index = 0; index < droppableItems.Length; index++) {
				if (itemChance <= droppableItems [index].chanceToDrop) {
					//drop item from this index;
					BuildNewItemDrop(index);
					break;
				} else {
					itemChance -= droppableItems [index].chanceToDrop;
				}
			}
		}
	}

	private float GetTotalChances() {
		float totalChances = 0.0f;
		foreach (DroppableItem element in droppableItems) {
			totalChances += element.chanceToDrop;
		}
		return totalChances;
	}

	private void BuildNewItemDrop(int itemIndex){
		GameObject newItem = Instantiate (droppableItems [itemIndex].itemPrototype, gameObject.transform.position, gameObject.transform.rotation);
	}

	public void Start(){
		myEnemyState = gameObject.GetComponent<EnemyState> ();
		totalChance = GetTotalChances ();
	}
}
