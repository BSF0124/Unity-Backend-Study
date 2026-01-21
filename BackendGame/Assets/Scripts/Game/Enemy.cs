using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameController gameController;

    public void Setup(GameController gameController)
    {
        this.gameController = gameController;
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnDie();
            gameController.GameOver();
        }
    }
}
