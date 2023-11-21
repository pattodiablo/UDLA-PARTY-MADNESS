using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   public float rotationSpeed = 180f; // Velocidad de rotación del tanque
    public float moveSpeed = 5f; // Velocidad de movimiento del tanque
    public float acceleration = 2f; // Aceleración del tanque
    
    public float maxMoveSpeed = 5f; // Velocidad máxima de movimiento del tanque
    private PlayerInput playerInput;
    public Transform cannon; // Referencia al objeto que representa el cañón del tanque
    public float cannonRotationSpeed = 90f; // Velocidad de rotación del cañón

    public GameObject cannonBulletPrefab;
      public Transform firePoint; // Punto desde el cual se instanciará la bala
 public float bulletSpeed = 10f; // Velocidad de la bala
  public float fireInterval = 1f; // Intervalo de tiempo entre disparos
   public float bulletLifetime = 3f; // Tiempo de vida de la bala

    Rigidbody playerRigidbody;
    void Start(){

        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
          InvokeRepeating("FireCannon", 0f, fireInterval);
    }

    
  void Update()
{
    var gamepad = Gamepad.current;
    if (gamepad == null)
        return; // No gamepad connected.

    Vector2 move1 = gamepad.leftStick.ReadValue();

    float joystickDistance = move1.magnitude;
    float calculatedMoveSpeed = Mathf.Clamp(joystickDistance, 0f, 1f) * maxMoveSpeed;

    if (joystickDistance > 0)
    {
        float rotationAngle = Mathf.Atan2(move1.x, move1.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -rotationAngle);

        // Smoother rotation using Quaternion.RotateTowards
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Movimiento
        Vector3 targetVelocity = Quaternion.AngleAxis(0f, Vector3.forward) * transform.up * joystickDistance * 20 / calculatedMoveSpeed;

        playerRigidbody.velocity = Vector3.Lerp(playerRigidbody.velocity, targetVelocity, acceleration * Time.deltaTime);
    }
    else
    {
        // If the joystick is not being used, stop the tank
        playerRigidbody.velocity = Vector3.zero;
    }

      Vector2 move2 = gamepad.rightStick.ReadValue();

         if (move2 != Vector2.zero)
        {
            float cannonRotation = Mathf.Atan2(move2.x, move2.y) * Mathf.Rad2Deg;
            Quaternion targetCannonRotation = Quaternion.Euler(0f, 0f, -cannonRotation);

            // Aplicar rotación suavizada al cañón
            cannon.rotation = Quaternion.Lerp(cannon.rotation, targetCannonRotation, cannonRotationSpeed * Time.deltaTime);
        }
}


  void FireCannon()
   {
        // Obtener la rotación actual del cañón
        Quaternion cannonRotation = cannon.transform.rotation;

        // Instanciar la bala en el punto de fuego (firePoint) con la rotación actual del cañón
        GameObject bullet = Instantiate(cannonBulletPrefab, firePoint.position, cannonRotation);

        // Obtener el componente Rigidbody de la bala y aplicarle una velocidad en la dirección del cañón
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Obtener la dirección en la que apunta el cañón y aplicar la velocidad
            Vector3 bulletDirection = cannonRotation * Vector3.up;
            bulletRigidbody.velocity = bulletDirection * bulletSpeed;
              Destroy(bullet, bulletLifetime);
        }
    }



}
       
        // 'Move' code here
    
