using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    public float moveSpeed = 2f;
    public float CircleScale = 5f;

    private int iteration = 90;

    private bool isLeft = false;

    public static Vector2 dir;

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        BulletCircle();
        if (Input.GetMouseButtonUp(0))
            Destroy(gameObject);
    }

    void BulletCircle()
    {
        transform.position = new Vector3(dir.x, dir.y - 2);
        dir = new Vector2(Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));
        if (isLeft == false)
        {
            transform.Translate(dir * (CircleScale * Time.deltaTime));
            iteration++;
        }
        else
        {
            transform.Translate(-dir * (CircleScale * Time.deltaTime));
            iteration--;
        }

        if (iteration >= 180 && isLeft == false)
        {
            isLeft = true;
        }
        if (iteration <= 0 && isLeft == true)
        {
            isLeft = false;
        }
    }
}
