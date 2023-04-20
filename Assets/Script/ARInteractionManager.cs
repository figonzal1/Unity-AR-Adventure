using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionManager : MonoBehaviour
{

	[SerializeField] private Camera arCamera;
	private ARRaycastManager arRayCastManager;
	private List<ARRaycastHit> hits = new List<ARRaycastHit>();

	private GameObject arPointer;
	private GameObject item3DModel;
	public GameObject Item3DModel
	{
		set
		{
			item3DModel = value;
			item3DModel.transform.position = arPointer.transform.position;
			item3DModel.transform.parent = arPointer.transform;
            isInitialPosition = true;
		}
	}
	private bool isInitialPosition;

	// Start is called before the first frame update
	void Start()
	{
		arPointer = transform.GetChild(0).gameObject;
		arRayCastManager = FindObjectOfType<ARRaycastManager>();
		GameManager.instance.OnMainMenu += SetItemPosition;
	}

	private void SetItemPosition()
	{
		if (item3DModel != null)
		{
			item3DModel.transform.parent = null;
			arPointer.SetActive(false);
			item3DModel = null;
		}
	}

	public void DeleteItem()
	{
		Destroy(item3DModel);
		arPointer.SetActive(false);
		GameManager.instance.MainMenu();
	}

	// Update is called once per frame
	void Update()
	{
		if (isInitialPosition)
		{
			Vector2 middlePoint = new Vector2(Screen.width / 2, Screen.height / 2);
			arRayCastManager.Raycast(middlePoint, hits, TrackableType.Planes);

			if (hits.Count > 0)
			{
				transform.position = hits[0].pose.position;
				transform.rotation = hits[0].pose.rotation;

				arPointer.SetActive(true);
				isInitialPosition = false;
			}
		}
	}
}
