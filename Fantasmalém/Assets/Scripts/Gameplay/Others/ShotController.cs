using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float speed = 20f;

    private Vector3 vel;

    void Update()
    {
        transform.position += vel * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)  
    {
        vel = direction.normalized * speed;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Shot" && other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Mimic")
        {
            Destroy(this.gameObject);
        }

        if (this.gameObject.tag == "EnemyShot" && other.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
        }
        
        if (this.gameObject.tag == "EnemyShot" && other.gameObject.tag == "Block")
        {
            Destroy(this.gameObject);
        }
    }
}
