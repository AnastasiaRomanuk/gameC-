using UnityEngine;

public class DieSpace : MonoBehaviour
{
    //если игрок падает с платформы, то на начало уровня ("долетает" до определенного места)
    public GameObject respawn;

    void OnTriggerEnter2D( Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = respawn.transform.position;
        }
    }
}
