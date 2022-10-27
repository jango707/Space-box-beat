using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 90));
            if (gameObject.tag == "box-left") {

                gameObject.tag = "box-right";
            }
            else
            {
                gameObject.tag = "box-left";
            }
        }
    }
}
