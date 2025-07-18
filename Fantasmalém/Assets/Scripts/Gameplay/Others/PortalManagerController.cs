using UnityEngine;

public class PortalManagerController : MonoBehaviour
{
    public EnemyController enemy;
    public SpawnerController spawner;
    public GameObject portal;
    public GameObject enemyPreFab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = enemyPreFab.GetComponent<EnemyController>();
        spawner = GetComponent<SpawnerController>();
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.kills >= spawner.wave) 
        {
            portal.SetActive(true);
        }
    }
}
