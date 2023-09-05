using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponent : MonoBehaviour
{
    // Start is called before the first frame update
       public GameObject objectToDestroy;

    void Start()
    {
      
           
    }
   public void Remove(){

     if (objectToDestroy != null)
            {
                // Destroy the GameObject
       Destroy(objectToDestroy);
               
         
            }
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
