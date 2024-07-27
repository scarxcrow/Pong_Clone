using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionController : MonoBehaviour
{
    public BallMovement ballMovement;
    public ScoreController scoreController;

    void BounceFromRacket(Collision2D coll)
    {
        Vector3 ballPos = this.transform.position;
        Vector3 racketPos = coll.gameObject.transform.position;

        float racketHeight = coll.collider.transform.position.y;

        float x;

        if(coll.gameObject.name == "RacketPlayer1")
        {
            x = 1;
        }
        else
        {
            x = -1;
        }

        float y = (ballPos.y - racketPos.y) / racketHeight;

        this.ballMovement.IncreaseHitCounter();
        this.ballMovement.MoveBall(new Vector2(x, y));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "RacketPlayer1" ||  collision.gameObject.name == "RacketPlayer2")
        {
            this.BounceFromRacket(collision);
        }
        else if(collision.gameObject.name == "WallLeft")
        {
            Debug.Log("Collision with left wall");
            this.scoreController.GoalPlayer2();
            StartCoroutine(this.ballMovement.StartBall(true));
        }
        else if(collision.gameObject.name == "WallRight")
        {
            Debug.Log("Collision with right wall");
            this.scoreController.GoalPlayer1();
            StartCoroutine(this.ballMovement.StartBall(false));
        }
    }
}
