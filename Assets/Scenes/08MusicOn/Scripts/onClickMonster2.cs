
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Events;
public class onClickMonster2 : MonoBehaviour
{

       public UnityEvent ClicktheMonster2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
         if (Input.GetMouseButtonDown(0))
    {

         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // El rayo ha golpeado un collider
            GameObject objetoGolpeado = hit.collider.gameObject;

            // Hacer algo con el objeto golpeado
           if (hit.collider.gameObject == this.gameObject)
            {
                // El rayo golpeó el objeto deseado  
            if (ClicktheMonster2 != null)
            {
                ClicktheMonster2.Invoke();  
                Debug.Log("Rayo golpeó el objeto deseado 2.");
            }

                
            }
   
          

            // Puedes agregar más lógica aquí, como llamar a una función en el objeto golpeado
            // objetoGolpeado.GetComponent<MiScript>().MiFuncion();
        }
        
    }
    }
}
