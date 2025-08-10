using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    PlayerController playerController;
    GunController gunController;

    float shieldTimer, projectilTimer, speedTimer, rechardeTimer;
    public int duration;

    //Escudo
    public bool protecion;

    //Tiro
    public Transform shotPrefab;
    public Transform bigShotPrefab;
    public float sizeIncrease = 0.2f;
    public bool bigShot;

    //Recarga
    int originalRecharge;
    public bool recharge;

    //SpeedBoost
    bool speedBoost;
    float originalMoveSpeed;

    //icons
    public GameObject[] icons;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();

        originalMoveSpeed = playerController.moveSpeed;
        originalRecharge = playerController.superShotTime;

        shieldTimer = 0;
        projectilTimer = 0;
        speedTimer = 0;
        rechardeTimer = 0;
    }

    void Update()
    {
        shieldTimer += Time.deltaTime;
        projectilTimer +=Time.deltaTime;
        speedTimer += Time.deltaTime;
        rechardeTimer += Time.deltaTime;
        StopShield();
        ResetShotSize();
        StopspeedBoost();
        StopRechard();
    }

    public void StartShield() 
    {
        icons[1].SetActive(true);
        shieldTimer = 0;
        protecion = true;      
        playerController.shields.SetActive(true); 
    }

    void StopShield() 
    {
        if (protecion == true && shieldTimer >= duration)
        {
            icons[1].SetActive(false);
            playerController.shields.SetActive(false);
            protecion = false;
            
        }
    }

    public void StartRechard()
    {
        rechardeTimer = 5;
        recharge = true;
        playerController.superTimer = 10f;
    }

    void StopRechard()
    {
        if (recharge == true && rechardeTimer >= 1)
        {
            playerController.shields.SetActive(false);
            recharge = false;
        }
    }

    public void SetSizeShot() 
    {
        icons[0].SetActive(true);
        gunController.shot = bigShotPrefab;
        bigShot = true;
        projectilTimer = 0;
    }

    void ResetShotSize()
    {
        if(bigShot == true && projectilTimer >= duration) 
        {
            icons[0].SetActive(false);
            bigShot = false;
            gunController.shot = shotPrefab;
        }
    }

    public void StartSpeedBoost() 
    {
        if (speedBoost != true) 
        {
            icons[2].SetActive(true);
            speedTimer = 0;
            speedBoost = true;
            playerController.moveSpeed *= 1.7f;
        }

    }

    public void StopspeedBoost()
    {
        if (speedBoost == true && speedTimer >= duration)
        {
            icons[2].SetActive(false);
            speedBoost = false;
            playerController.moveSpeed = originalMoveSpeed;

        }
    }
}
