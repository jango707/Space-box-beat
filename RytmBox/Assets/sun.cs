using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour{
    public float rate = 0.01f;
    void Start()
    {
        StartCoroutine(Shrink());
    }

    private void Update()
    {
        if(transform.localScale.x < 0)
        {
            GameManager.Instance.endGame();
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(gameObject.tag) && gameObject.transform.localScale.x < 1.25f)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        if (!collision.CompareTag(gameObject.tag))
        {
            GameManager.Instance.missedShots++;
            gameObject.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        }

        Destroy(collision.gameObject);
    }

    private IEnumerator Shrink()
    {
        if(gameObject.transform.localScale.x > 0 && GameManager.Instance.isPlaying()) gameObject.transform.localScale -= new Vector3(rate, rate, rate);
        yield return new WaitForSeconds(.05f);
        StartCoroutine(Shrink());
    }
}
