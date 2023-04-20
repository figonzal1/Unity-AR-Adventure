using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{
	private string itemName;
	private string itemDescription;
	private Sprite itemImage;
	private GameObject item3DModel;

	public string ItemName { set => itemName = value;  }
	public Sprite ItemImage { set => itemImage = value; }
	public string ItemDescription { set => itemDescription = value;  }
	public GameObject Item3DModel { set => item3DModel = value; }

	// Start is called before the first frame update
	void Start()
	{
		transform.GetChild(0).GetComponent<Text>().text = itemName;
		transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
		transform.GetChild(2).GetComponent<Text>().text = itemDescription;

        var button = GetComponent<Button>();

        //Cambiar ventana
        button.onClick.AddListener(GameManager.instance.ARPosition);

        //Crear objeto 3d
        button.onClick.AddListener(Create3DModel);
	}

	private void Create3DModel()
	{
		Instantiate(item3DModel);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
