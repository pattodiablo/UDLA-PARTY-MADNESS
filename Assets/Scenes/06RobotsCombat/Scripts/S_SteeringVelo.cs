using Cinemachine;
using UnityEngine;
using UnityEngine.UI;   
public class S_SteeringVelo : MonoBehaviour
{
    private GameObject Player;
    public GameObject JoyHandle;

    [SerializeField] private float speed = 15f;
  
    public Vector3 inputDirection;
    private Vector2 startPosEnPixeles;
    private Vector3 startJoyPositionPixels;
    private Vector3 startJoyPositionWorld;
    private Vector2 movedPositionPixeles;
    private RectTransform rectTransform;

    public float joyMaxDistance = 200f;

    private Vector2 movedPosition;
    public float distanceForCameraChange = 120f;

   private float distance;
    private int PlayerVelo = 0;
    private int extraSpeed = 1;
    Rigidbody playerRigidbody;
    public float velocitySmoothingSpeed = 10.0f;

    public float rotationSpeed = 10.0f;

    private Image joyHandleImage;
    private Image joyBaseImage;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("TankPlayer");
        rectTransform = GetComponent<RectTransform>();
        playerRigidbody = Player.GetComponent<Rigidbody>();

        joyHandleImage = JoyHandle.GetComponent<Image>();
        joyBaseImage = GetComponent<Image>();
    }

 


    private void FixedUpdate()
    {
         if (Input.touchCount > 0)
        {
       
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            PlayerVelo = 1;
            startPosEnPixeles = transform.position = new Vector2(touch.position.x, touch.position.y);
            Debug.Log(startJoyPositionPixels);
            Quaternion playerRotation = Player.transform.rotation;
            Vector3 playerRotationEuler = playerRotation.eulerAngles;
                joyHandleImage.enabled = true;
                joyBaseImage.enabled = true;
          
        }
        else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
                
            Vector2 movedPositionPixeles = new Vector2(touch.position.x, touch.position.y);        
            inputDirection = (movedPositionPixeles - startPosEnPixeles).normalized;
            distance = Vector3.Distance(startPosEnPixeles, movedPositionPixeles);          

            if (distance >= distanceForCameraChange)
            {
           
                extraSpeed = 2;
            }
            else
            {
          
                extraSpeed = 1;
            }

            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg - 90f;
      
           // rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            Quaternion targetRotation = Quaternion.Euler(0, -angle - 90f, 0);

                if (Player!=null)
                {
                    Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }

                Vector3 fixedVelocityDirection = new Vector3(inputDirection.x,0,inputDirection.y);
           
             if (distance > joyMaxDistance)
                {
                    distance = joyMaxDistance;
                }
    
           
            Vector2 StarPos3d = new Vector2(startPosEnPixeles.x,startPosEnPixeles.y);
            float angleRadians = -angle * Mathf.Deg2Rad;
            movedPosition = new Vector2(distance * Mathf.Sin(angleRadians), distance * Mathf.Cos(angleRadians)) + StarPos3d;
            JoyHandle.transform.position =  movedPosition;
        }
    
        else if (touch.phase == TouchPhase.Ended)
        {     
      
            transform.position = new Vector2(touch.position.x, touch.position.y);
            JoyHandle.transform.position = new Vector2(touch.position.x, touch.position.y);
            joyHandleImage.enabled = false;
            joyBaseImage.enabled = false;
            extraSpeed = 0;
        }         
     }else{

           joyHandleImage.enabled = false;
           joyBaseImage.enabled = false;
     }

        if (Player != null)
        {
            Vector3 targetVelocity = Quaternion.AngleAxis(90f, Vector3.up) * Player.transform.forward * distance / speed * extraSpeed;
            playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, targetVelocity, velocitySmoothingSpeed * Time.deltaTime);
        }
      
    }
}