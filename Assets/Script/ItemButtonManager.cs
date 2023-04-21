using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
	private string itemName;
	private string itemDescription;
	private Sprite itemImage;
	private GameObject item3DModel;
	private ARInteractionManager interactionManager;
	private string urlBundelModel;
	private RawImage imageBundle;

	public string ItemName { set => itemName = value; }
	public Sprite ItemImage { set => itemImage = value; }
	public string ItemDescription { set => itemDescription = value; }
	public GameObject Item3DModel { set => item3DModel = value; }

	public string URLBundelModel {set => urlBundelModel = value;}
	public RawImage ImageBundle { get => imageBundle; set => imageBundle = value;}

	// Start is called before the first frame update
	void Start()
	{
		transform.GetChild(0).GetComponent<Text>().text = itemName;
		//transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
		imageBundle = transform.GetChild(1).GetComponent<RawImage>();
		transform.GetChild(2).GetComponent<Text>().text = itemDescription;

		var button = GetComponent<Button>();

		//Cambiar ventana
		button.onClick.AddListener(GameManager.instance.ARPosition);

		//Crear objeto 3d
		button.onClick.AddListener(Create3DModel);

		interactionManager = FindObjectOfType<ARInteractionManager>();
	}

	private void Create3DModel()
	{
		//interactionManager.Item3DModel = Instantiate(item3DModel);
		StartCoroutine(DownloadAssetBundle(urlBundelModel));

	}

	IEnumerator DownloadAssetBundle(string urlAssetBundle){
		UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(urlAssetBundle);
		yield return request.SendWebRequest();

		if(request.result == UnityWebRequest.Result.Success){
			AssetBundle model3D =  DownloadHandlerAssetBundle.GetContent(request);

			if(model3D != null){
				interactionManager.Item3DModel = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0]) as GameObject);
			}else {
				Debug.Log("Not a valid asset bundle");
			}

		}else {
			Debug.Log("Error loading asset");
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
