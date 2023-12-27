using UnityEngine;

public class MovimientoObjeto : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float velocidadRotacion = 100f;
    public float limiteDerecho = 10f;
    public float limiteIzquierdo = -10f;

    public AudioClip clickSound; // Asigna el clip de sonido en el Inspector.

    public GameObject objetoRotacion;
    public GameObject destroyEffectPrefab;
 public GameObject plusTenPrefab;
    

    private GameObject obJectPlusTen; // Asigna el Animator en el Inspector.

    private string[] nombresSprites = { "trash1", "trash2", "trash3", "trash4" };

     private AudioSource audioSource;

     private Animator plusTenTextAnimator;

    // Busca la cámara con el tag "MainCamera" al iniciar el juego
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
      
       obJectPlusTen = GameObject.Find("PlusTenText");
        if (obJectPlusTen != null)
        {
         plusTenTextAnimator = obJectPlusTen.GetComponent<Animator>();
        }
        if (mainCamera == null)
        {
            Debug.LogError("No se encontró una cámara con el tag 'MainCamera'. Asigna el tag correctamente a la cámara principal.");
        }

      
    }

    void Update()
    {
        // Mueve el objeto en el eje X (traslación)
        transform.Translate(Vector3.right * velocidadMovimiento * Time.deltaTime);

        // Rota el objeto alrededor de su propio eje Z (rotación)
        objetoRotacion.transform.Rotate(Vector3.forward, -velocidadRotacion * Time.deltaTime);

        // Si el objeto sale por la derecha, vuelve a colocarlo en la izquierda
        if (transform.position.x > limiteDerecho)
        {
            transform.position = new Vector3(limiteIzquierdo, transform.position.y, transform.position.z);
        }

        // Manejo de la entrada táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
            
               
                // Convierte la posición del toque a un rayo en el mundo
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
ReproducirSonidoClick();
                    plusTenTextAnimator.Play("PlusTenText");
                // Comprueba si el rayo intersecta con el objeto
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
                {  
                    
                    Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity).transform.localScale = new Vector3(2f, 2f, 2f);
                     Instantiate(plusTenPrefab, transform.position, Quaternion.identity).transform.localScale = new Vector3(2f, 2f, 2f);
                    Destroy(gameObject);
                }
            }
        }
    }

     void ReproducirSonidoClick()
    {
        // Verifica si hay un AudioClip asignado
        if (clickSound != null)
        {
            // Reproduce el sonido
            audioSource.PlayOneShot(clickSound);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si la colisión es con un objeto de etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Crea la instancia del efecto de destrucción
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity).transform.localScale = new Vector3(2f, 2f, 2f);

            // Destruye este objeto
            Destroy(gameObject);
        }
    }

}
