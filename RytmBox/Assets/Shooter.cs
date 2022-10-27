using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject shotPrefab;
    public GameObject shotPrefab2;
    public GameObject shooterOutline;

    public float bpm = 0;

    private Color currentColor;

    void Start()
    {
        StartCoroutine(AlternateColor());
    }

    private void Update()
    {
        if (UnityEngine.Random.Range(0, 10000) < 300) onOnbeatDetected();
    }

    public void onOnbeatDetected()
    {
        if (GameManager.Instance.isPlaying())
        {
            GameManager.Instance.GetComponent<AudioSource>().Play();
            GameObject shot = currentColor == Color.green ? shotPrefab : shotPrefab2;
            GameObject travellingShot = GameObject.Instantiate(shot, gameObject.transform.position, Quaternion.identity);
            GameManager.Instance.shotCount++;
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        Color c = currentColor;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            shooterOutline.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.05f);
        }
    }

    private IEnumerator AlternateColor()
    {
        currentColor = currentColor == Color.green ? Color.magenta : Color.green;
        onOnbeatDetected();
        yield return new WaitForSeconds(60f/bpm);
        StartCoroutine(AlternateColor());
    }
}
