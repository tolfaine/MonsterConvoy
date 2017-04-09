using UnityEngine;

public class MapCamera2D : MonoBehaviour
{
	//The map
	public Transform Area;

	//Sprite details
	 Sprite sprite;
	 float pixelUnits;
	 Vector2 size;
	 Vector2 offset;

	//Camera bounds
	 float left;
	 float right;
	 float top;
	 float bottom;
	 float maxZoom;
	 float minZoom;

	Vector3 movement;
	Vector3 currentMousePos; 
	Vector3 prevMousePos;
	Vector3 mouseDelta;
	public float speed = 20.0f; 

	public float camZoomSpeed = 0.5f; 
	Vector3 camZoom;

    bool foundPlayer = false;

	public void Start()
	{
		sprite = Area.transform.GetComponent<SpriteRenderer> ().sprite;

		//Calculate the pixel per unit value is for this sprite
		pixelUnits = sprite.rect.width / sprite.bounds.size.x;

		//Calculate the size and offset of the background sprite
		size = new Vector2 (Area.transform.localScale.z * sprite.texture.width / pixelUnits,
			Area.transform.localScale.y * sprite.texture.height / pixelUnits);
		offset = Area.transform.position;

//		Update ();

		//Position the camera at the center of the background image
		Vector2 position = Area.transform.position;
		Vector3 camPosition = position;
		Vector3 point = Camera.main.WorldToViewportPoint (camPosition);

        Vector3 delta = camPosition - Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z));
		Vector3 destination = transform.position + delta;
		transform.position = destination;
        camZoom.z = 5;
	}
		
	//Get zoom constraints and zoom as large as possible for current view
	private void Update()
	{
        if(!foundPlayer)
        {
            foundPlayer = true;
            transform.position = Area.GetComponent<MapPlayer>().player.transform.position + Vector3.back;
        }
		currentMousePos = Input.mousePosition; 
		mouseDelta = currentMousePos - prevMousePos;
		movement = mouseDelta; 
		movement = movement.normalized * speed * Time.deltaTime;

		if (Input.GetMouseButton (0))
			transform.position -= movement;
		
		//Calculate the current ratio
		float w = Screen.width/size.x;
		float h = Screen.height / size.y;
		float ratio = w / h;
		float ratio2 = h / w;
		maxZoom = size.y / 2;
		if (ratio2 <= ratio)
			maxZoom /= ratio;
		minZoom = 1;
        	
		camZoom -= Input.mouseScrollDelta.y * Vector3.forward * camZoomSpeed;
		camZoom.z = Mathf.Clamp (camZoom.z, minZoom, maxZoom);

		Camera.main.orthographicSize = camZoom.z;

		RefreshBounds ();
	}

	//Calculate the max distance the camera can travel
	private void RefreshBounds()
	{
		var vertExtent = Camera.main.orthographicSize;
		var horzExtent = vertExtent * Screen.width / Screen.height;

		left = horzExtent - size.x / 2.0f + offset.x;
		right = size.x / 2.0f - horzExtent + offset.x;

		bottom = vertExtent - size.y / 2.0f + offset.y;
		top = size.y / 2.0f - vertExtent + offset.y;
	}

	public void LateUpdate()
	{
		//Clamp camera inside of our bounds
		Vector3 v3 = transform.position;
		v3.x = Mathf.Clamp (v3.x, left, right);
		v3.y = Mathf.Clamp (v3.y, bottom, top);
		transform.position = v3;

		prevMousePos = currentMousePos;
	}
}