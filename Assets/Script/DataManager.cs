using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	[SerializeField]
	private List<Item> items = new List<Item>();

	[SerializeField]
	private GameObject buttonContainer;

	[SerializeField]
	private ItemButtonManager itemButtonManager;

	// Start is called before the first frame update
	void Start()
	{
		//Suscribir al evento
		GameManager.instance.OnItemsMenu += CreateButtons;
	}

	private void CreateButtons()
	{
		foreach (var item in items)
		{
			ItemButtonManager itemButton;
			itemButton = Instantiate(itemButtonManager, buttonContainer.transform);

			//Llenar datos
			itemButton.ItemName = item.ItemName;
			itemButton.ItemImage = item.ItemImage;
			itemButton.ItemDescription = item.ItemDescription;
			itemButton.Item3DModel = item.Item3DModel;
			itemButton.name = item.ItemName;

		}

		//Remover suscripcion al evento
		GameManager.instance.OnItemsMenu -= CreateButtons;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
