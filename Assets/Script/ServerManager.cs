using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : MonoBehaviour
{

	[SerializeField] private string jsonURL;
	[SerializeField] private ItemButtonManager itemButtonManager;
	[SerializeField] private GameObject buttonsContainer;

	[Serializable]
	public struct Items
	{

		[Serializable]
		public struct Item
		{
			public string Name;
			public string Description;
			public string URLBundleModel;
			public string URLImageModel;
		}

		public Item[] items;
	}

	public Items newItems = new Items();

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GetJsonData());
		GameManager.instance.OnItemsMenu += CreateButtons;
	}

	private void CreateButtons()
	{
		foreach (var item in newItems.items)
		{

			ItemButtonManager itemButton;
			itemButton = Instantiate(itemButtonManager, buttonsContainer.transform);
			itemButton.ItemName = item.Name;
			itemButton.ItemDescription = item.Description;
			itemButton.URLBundelModel = item.URLBundleModel;
			StartCoroutine(GetBundleImage(item.URLImageModel,itemButton));

			GameManager.instance.OnItemsMenu -= CreateButtons;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator GetJsonData()
	{
		UnityWebRequest request = UnityWebRequest.Get(jsonURL);

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			newItems = JsonUtility.FromJson<Items>(request.downloadHandler.text);
		}
		else
		{
			Debug.LogError("Error downloading");
		}
	}

	IEnumerator GetBundleImage(string urlImage, ItemButtonManager button)
	{

		UnityWebRequest request = UnityWebRequest.Get(urlImage);
		request.downloadHandler = new DownloadHandlerTexture();

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			button.ImageBundle.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
		}
		else
		{
			Debug.LogError("Error downloading");
		}
	}
}
