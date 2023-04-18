using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

	[SerializeField] private GameObject mainMenuCanvas;

	[SerializeField] private GameObject itemsMenuCanvas;

	[SerializeField] private GameObject arPositionCanvas;


	// Start is called before the first frame update
	void Start()
	{
		GameManager.instance.OnMainMenu += ActivateMainMenu;
		GameManager.instance.OnItemsMenu += ActivateItemsMenu;
		GameManager.instance.OnARPosition += ActivateARPositionMenu;
	}

	private void ActivateMainMenu()
	{
		//Mostrar botones de main menu
		mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
		mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
		mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);

		//Ocultar botones de items menu
		itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
		itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
		itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

		//Ocultar botones de ar position
		arPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
		arPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
	}

	private void ActivateItemsMenu()
	{
		//ocultar botones de main menu
		mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
		mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
		mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

		//Mostrar botones de items menu
		itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
		itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
		itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(300, 0.3f);
	}

	private void ActivateARPositionMenu()
	{
		//Ocultar botones de main menu
		mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
		mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
		mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

		//Ocultar botones de items menu
		itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
		itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
		itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

		//Mostrar botones de ar position
		arPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
		arPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
	}
}
