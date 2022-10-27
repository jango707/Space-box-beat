using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{

    public float speed;
    private void Start()
    {
        speed = GameManager.Instance.shotspeed;
    }
    void Update()
    {
        gameObject.transform.Translate(0, speed * Random.Range(1, 1.3f) * Time.deltaTime, 0);
        if(gameObject.transform.position.y > 10 || Mathf.Abs(gameObject.transform.position.x) > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("box-right"))
        {
            gameObject.transform.Rotate(0, 0, -90);
        } else if (collision.gameObject.CompareTag("box-left"))
        {
            gameObject.transform.Rotate(0, 0, 90);
        }
    }

}
