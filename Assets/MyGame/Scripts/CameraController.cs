using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float dragSpeed = 2f; // Speed of dragging
	[SerializeField] private float zoomSpeedTouch = 0.1f; // Zoom speed for touch input
	[SerializeField] private float zoomSpeedMouse = 1f; // Zoom speed for mouse input
	[SerializeField] private float minZoom = 1f; // Minimum zoom level
	[SerializeField] private float maxZoom = 5f; // Maximum zoom level
	[SerializeField] private float minX = -1f; // Minimum X position
	[SerializeField] private float maxX = 1f; // Maximum X position
	[SerializeField] private float minY = -1f; // Minimum Y position
	[SerializeField] private float maxY = 1f; // Maximum Y position

	private Vector3 dragOrigin; // Origin position of dragging
	private bool isDragging = false; // Flag to indicate dragging

	private void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		if (Input.touchCount == 2)
		{
			HandleZoom(Input.GetTouch(0), Input.GetTouch(1));
		}
		else
		{
			HandleMouseInput();
		}
	}

	private void HandleZoom(Touch touch1, Touch touch2)
	{
		if (touch1.phase != TouchPhase.Moved && touch2.phase != TouchPhase.Moved)
			return;

		Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
		Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

		float prevMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
		float currentMagnitude = (touch1.position - touch2.position).magnitude;

		float difference = currentMagnitude - prevMagnitude;

		ZoomCamera(difference * zoomSpeedTouch);
	}

	private void HandleMouseInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			BeginDrag();
		}

		if (Input.GetMouseButtonUp(0))
		{
			EndDrag();
		}

		if (isDragging)
		{
			DragCamera();
		}

		if (Input.mouseScrollDelta.y != 0)
		{
			ZoomCamera(Input.mouseScrollDelta.y * zoomSpeedMouse);
		}
	}

	private void BeginDrag()
	{
		dragOrigin = Input.mousePosition;
		isDragging = true;
	}

	private void EndDrag()
	{
		isDragging = false;
	}

	private void DragCamera()
	{
		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

		transform.Translate(move, Space.World);
		ClampCameraPosition();
	}

	private void ClampCameraPosition()
	{
		Vector3 clampedPosition = transform.position;
		clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
		clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
		transform.position = clampedPosition;
	}

	private void ZoomCamera(float zoomAmount)
	{
		float newSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount, minZoom, maxZoom);
		Camera.main.orthographicSize = newSize;
	}
}
