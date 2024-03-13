using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float Speed;
    public Vector2 dir;

    public Rigidbody2D rigid;
    //public InGameManager manager;

    private bool goingLeft;
    private bool goingDown;
    private bool goingRight;

    Camera cam;
    private bool isStop = false;

    // Start is called before the first frame update
    void Awake()
    {
        isStop = false;
        cam = Camera.main;
        transform.position = new Vector3(0, -3, 0);
        dir = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
            Shot();
        if (isStop)
            Movement();
    }

    void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStop = true;
            dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir * Speed, ForceMode2D.Impulse);
            Speed = 1000;
        }
        

    }

    void Movement()
    {
        rigid.velocity = dir * Speed * Time.deltaTime;

        if (transform.position.x > 5 && !goingLeft)
        {
            dir = new Vector2(-dir.x, dir.y);
            goingLeft = true;
            goingRight = false;
        }
        if (transform.position.x < -5 && !goingRight)
        {
            dir = new Vector2(-dir.x, dir.y);
            goingLeft = false;
            goingRight = true;
        }
        if (transform.position.y > 3 && !goingDown)
        {
            dir = new Vector2(dir.x, -dir.y);
            goingDown = true;
        }
        if (transform.position.y < -4)
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        isStop = false;
        Speed = 0;
        rigid.velocity = dir * Speed * Time.deltaTime;
        goingDown = false;
        goingLeft = false;
        goingRight = false;
        transform.position = new Vector3(transform.position.x, -3, 0);
    }
}
