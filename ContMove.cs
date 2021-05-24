
using UnityEngine;

public class ContMove : MonoBehaviour
{
    public GameObject player;//игрок
   
    //слежение камеры за игроком
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, -10f);
    }
}
