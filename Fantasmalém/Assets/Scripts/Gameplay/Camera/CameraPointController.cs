using UnityEngine;

public class CameraPointController : MonoBehaviour
{
    public GameObject follow; // O que o ponto vai seguir
    public float smoothness; // A sensibilidade do movimento
    public Vector3 distance;

    Vector3 targetPosition;

    Vector3 offset; // A distancia do objeto

    public PlayerController playerController;

    void Start()
    {
        //offset = follow.transform.position + distance; // Calculo da posição
        TargetMovement();
    }

    // Update is called once per frame
    void Update()
    {
        CamPointMovement();
    }

    void CamPointMovement()
    {
        if (playerController.camMovement != true)
        {
            TargetMovement();

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, smoothness * Time.deltaTime);
        }
    }

    void TargetMovement() 
    {
        targetPosition = new Vector3(follow.transform.position.x + distance.x, transform.position.y, follow.transform.position.z + distance.z);
    }
}
