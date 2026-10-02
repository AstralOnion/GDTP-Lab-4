using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    private int hitCount = 0;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.5f * Time.deltaTime * Vector3.down);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        //changed if-else statement to switch statement.
        switch(whatIHit.tag)
        {
            case "Player":
                GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
                Destroy(whatIHit.gameObject);
                break;
            case "Laser":
                hitCount++;
                Destroy(whatIHit.gameObject);
                //moved the check for meteor destruction to the switch to avoid checking it every frame in Update.
                if (hitCount >= 5)
                {
                    Destroy (this.gameObject);
                }
                break;
        }
    }
}
