using UnityEngine;

public class FollowFinger : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject cuerpoTaladro;
    private bool isDragging = false;

    private float zpostiion;
    private void Start()
    {
           zpostiion = cuerpoTaladro.transform.position.z;
     //   mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                
                    // Check if the touch began on our GameObject
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                  
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == cuerpoTaladro)
                    {
                        
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        // Move the GameObject with the finger
                        Vector3 touchPos = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y , 450f));
                        transform.position = touchPos;
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }
}
