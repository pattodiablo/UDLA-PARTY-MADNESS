using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class DestroyAnimWhenFinish : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObjectToDdestroy;
  public void onFinishDestroy(string s){

    Destroy(gameObjectToDdestroy);
  }
}
