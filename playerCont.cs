using UnityEngine;
using UnityEngine.SceneManagement;//для работы со сценами

public class playerCont : MonoBehaviour
{
    public float speed = 20f;//скорость объекта
    private Rigidbody2D rb;
    Animator anim;
    int life = 5;//жизни в начале уровня

    private bool faceRight = true;//повернуть героя

    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

  
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");//диапозон от -1 до 1(если -> ,то эта переменная плавно принимает значение от 0 до 1)
        rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);//идем в право с указанной скоростью по кадрово

        if (Input.GetKeyDown(KeyCode.Space))//нажат пробел для прыжка
            rb.AddForce(Vector2.up * 5000);//умножаем на силу для прыжка

        //поворот картинки в зависимости от направления
        if (moveX > 0 && !faceRight)//идем вправо,а смотрим влево
            flip();
        else if (moveX < 0 && faceRight)
            flip();

        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetInteger("hero", 1);//если герой бежит,анимация бега
        }
    }

    //поворот картинки
    void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

    //столконовение с источником жизни
    void OnTriggerEnter2D(Collider2D shit)
    {
        if (shit.gameObject.tag == "life")//проверка столкновения
        {
            life++;//пополнение жизни
            Destroy(shit.gameObject);//уничтожение
        }
    }

    //столкновением с врагом
    void OnCollisionEnter2D(Collision2D shit)
    {
        if (shit.gameObject.tag == "vs")//проверка столкновения
        {
            life--;
            //если жизни закончились,то на начало
            if (life == 0)
            {
                Invoke("ReloadLevel", 0);//вызов перезагрузки сцены
            }
        }
    }

    void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);//метод перезагрузки сцены(уровня)
    }

    //отображение кол-ва жизней
    void OnGUI()
    {
        GUI.Box(new Rect(15, 15, 100, 30), "life = " + life);
    }
}
