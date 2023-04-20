using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionManager : MonoBehaviour
{

	[SerializeField] private Camera arCamera;
	private ARRaycastManager arRayCastManager;
	private List<ARRaycastHit> hits = new List<ARRaycastHit>();

	private GameObject arPointer;
	private GameObject item3DModel;
	private GameObject itemSelected;
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
	private bool isOverUI;

	private Vector2 initialTouchPos;
	private bool isOver3DModel;

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

			Debug.Log("Set Item position");
		}
	}

	public void DeleteItem()
	{
		Destroy(item3DModel);
		arPointer.SetActive(false);
		GameManager.instance.MainMenu();

		Debug.Log("Delete item");
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


		//Si se ha tocadola pantalla
		if (Input.touchCount > 0)
		{
			Touch touchOne = Input.GetTouch(0);

			//Cuando comienza el touch
			if (touchOne.phase == TouchPhase.Began)
			{
				var touchPosition = touchOne.position;
				isOverUI = isTapOverUI(touchPosition);
				isOver3DModel = isTapOver3DModel(touchPosition);
			}


			if (touchOne.phase == TouchPhase.Moved)
			{
				if (arRayCastManager.Raycast(touchOne.position, hits, TrackableType.Planes))
				{
					Pose hitPose = hits[0].pose;

					if (!isOverUI && isOver3DModel)
					{
						transform.position = hitPose.position;
					}
				}
			}

			//Rotar modelos 3d
			if (Input.touchCount == 2)
			{
				Touch touchTwo = Input.GetTouch(1);

				if (touchOne.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began)
				{
					initialTouchPos = touchTwo.position - touchOne.position;
				}

				if (touchOne.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved)
				{
					Vector2 currentTouchPos = touchTwo.position - touchOne.position;
					float angle = Vector2.SignedAngle(initialTouchPos, currentTouchPos);
					item3DModel.transform.rotation = Quaternion.Euler(0, item3DModel.transform.eulerAngles.y - angle, 0);
					initialTouchPos = currentTouchPos;
				}
			}


			if(isOver3DModel && item3DModel == null  && !isOverUI) {
				GameManager.instance.ARPosition();
				item3DModel = itemSelected;
				itemSelected = null;
				arPointer.SetActive(true);
				transform.position = item3DModel.transform.position;

				item3DModel.transform.parent = arPointer.transform;
			}
		}
	}

	private bool isTapOver3DModel(Vector2 touchPosition)
	{
		Ray ray = arCamera.ScreenPointToRay(touchPosition);

		if(Physics.Raycast(ray,out RaycastHit hit3DModel)){

			if(hit3DModel.collider.CompareTag("Item")) {

				itemSelected = hit3DModel.transform.gameObject;
				return true;
			}
		}
		return false;
	}

	private bool isTapOverUI(Vector2 touchPosition)
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		eventData.position = new Vector2(touchPosition.x, touchPosition.y);

		List<RaycastResult> result = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, result);

		return result.Count > 0;
	}
}
