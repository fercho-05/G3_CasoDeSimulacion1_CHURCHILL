using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int bulletsCollided = 0; 
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet" && bulletsCollided == 0)
        {
            bulletsCollided++;
            StateManager.Instance.incrementScore();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

   


}
