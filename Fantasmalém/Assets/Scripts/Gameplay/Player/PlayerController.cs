using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed, maxSpeed, drag;
    private bool foward, backward, left, right;

    public GameObject wParticle;

    public float minX = -10f;
    public float minZ = -10f;

    public float maxX = 10f;
    public float maxZ = 10f;

    public int superShotTime;
    public float superTimer;

    public bool camMovement;

    public GunController gun; // Arma

    public LifeController lifeController; // Vida
    public int playerDamage;
    public GameObject hitParticle;

    public EnemyController enemy; // Inimigo

    public GameObject shields;
    public PowerUpController powerUp;

    public InterfaceController interfaceController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camMovement = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        wParticle.SetActive(false);
        superTimer = 10f;
        shields.SetActive(false);
        interfaceController = FindAnyObjectByType<InterfaceController>();

        interfaceController.LaserIndicator(superTimer, superShotTime);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (foward == true || backward == true || left == true || right == true)
        {
            wParticle.SetActive(true);
        }
        else 
        { 
            wParticle.SetActive(false);
        }

        LimitVelocity();
        HandleDrag();

        RotateTowardsMouse();

        superTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.timeScale == 1)
            {
                gun.Shoot();
            }
        }

        if ( superTimer >= superShotTime)
        {
            if (Input.GetMouseButtonDown(1)) 
            {
                if (Time.timeScale == 1)
                {
                    gun.SuperShot();
                    superTimer = 0;
                }
            }

            interfaceController.LaserIndicator(superTimer, superShotTime);
        }
    }

    private void FixedUpdate()
    {
        if (foward)
        {
            rb.AddForce(moveSpeed * Vector3.forward);
            foward = false;
        }

        if (backward)
        {
            rb.AddForce(moveSpeed * Vector3.back);
            backward = false;
        }

        if (right)
        {
            rb.AddForce(moveSpeed * Vector3.right);
            right = false;
        }

        if (left)
        {
            rb.AddForce(moveSpeed * Vector3.left);
            left = false;
        }
    }

    public void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        transform.position = clampedPosition;
    }

    void Movement()
    {

        if (Input.GetKey(KeyCode.W))
        {
            foward = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            backward = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }

    }

    void LimitVelocity()
    {
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitation = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitation.x, rb.linearVelocity.y, limitation.z);
        }
    }

    void HandleDrag()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z) / (1 + drag / 100) + new Vector3(0, rb.linearVelocity.y, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyShot" && powerUp.protecion != true)
        {
            lifeController.Hit(enemy.damage, this.gameObject);
        }
    }
    
    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y;

            Vector3 direction = (target - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
            }
        }
    }

    public void HitInstance()
    {
        GameObject hit = Instantiate(hitParticle, this.transform.position, Quaternion.identity);
        Destroy(hit, 1f);
    }
}
