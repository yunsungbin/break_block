using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float Speed;
    public Vector2 dir;

    public Rigidbody2D rigid;
    //public InGameManager manager;

    private bool goingLeft; //오른쪽에 맞았을때
    private bool goingDown; //위에 맞았을때
    private bool goingRight; //왼쪽에 맞았을때

    Camera cam;
    private bool isStop = false;
    private bool isCopy = false;

    [SerializeField]
    private GameObject ballRot;
    float angle;

    // Start is called before the first frame update
    void Awake()
    {
        isStop = false;
        cam = Camera.main;
        transform.position = new Vector3(0, -3, 0);
        dir = Vector2.down;
        rigid = GetComponent<Rigidbody2D>();
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
            Instantiate(ballRot, new Vector3(-2, -5, 0), Quaternion.identity);
        }
        if (Input.GetMouseButton(0))
        {
            
            dir = BallRot.dir;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isStop = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir * Speed, ForceMode2D.Impulse);
            Speed = 500;
        }

    }

    void Movement()
    {
        rigid.velocity = dir.normalized * Speed * Time.deltaTime * 10;

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
        if (transform.position.y < -3)
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
