using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(2f * Time.deltaTime * Vector3.down);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        //changed if-else statement to switch statement.
        switch (whatIHit.tag)
        {
            case "Player":
                GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
                Destroy(whatIHit.gameObject);
                Destroy(this.gameObject);
                break;
            case "Laser":
                GameObject.Find("GameManager").GetComponent<GameManager>().meteorCount++;
                Destroy(whatIHit.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }
}
