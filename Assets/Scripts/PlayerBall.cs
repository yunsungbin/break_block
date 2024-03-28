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

    private float time;
    private float timer = 5;

    public static Vector3 playrDir;

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
            return;
        if (isStop)
        {
            Movement();
            MoveTime();
        }  
    }

    public void TouchDown()
    {
        playrDir = transform.position;
        Instantiate(ballRot, new Vector3(playrDir.x, playrDir.y, 0), Quaternion.identity);

    }

    public void Touch()
    {
        dir = BallRot.dir;
    }

    public void TouchUp()
    {
        isStop = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir * Speed, ForceMode2D.Impulse);
        Speed = 500;
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
        transPosCheck();

        isStop = false;
        Speed = 0;
        rigid.velocity = dir * Speed * Time.deltaTime;

        goingDown = false;
        goingLeft = false;
        goingRight = false;

        time = 0;
        
    }

    void transPosCheck()
    {
        if (transform.position.x > 5)
            transform.position = new Vector3(4.9f, -3, 0);
        else if (transform.position.x < -5)
            transform.position = new Vector3(-4.9f, -3, 0);
        else
            transform.position = new Vector3(transform.position.x, -3, 0);
    }

    void MoveTime()
    {

        if(time > timer)
        {
            Speed *= 2;
            time = 0;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
