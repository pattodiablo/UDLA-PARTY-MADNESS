using UnityEngine;

public class DragObjectMobile3D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private RaycastHit hit;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Obtener el primer toque

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Comenzar a arrastrar cuando se toca el objeto
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        offset = transform.position - hit.point;
                    }
                    break;

                case TouchPhase.Moved:
                    // Arrastrar el objeto
                    if (isDragging)
                    {
                        Vector3 touchPos = ray.GetPoint(hit.distance);
                        transform.position = new Vector3(touchPos.x + offset.x, touchPos.y + offset.y, touchPos.z + offset.z);
                    }
                    break;

                case TouchPhase.Ended:
                    // Dejar de arrastrar cuando se levanta el dedo
                    isDragging = false;
                    break;
            }
        }
    }
}
