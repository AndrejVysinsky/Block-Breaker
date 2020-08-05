using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TrajectoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject TrajectoryPointPrefeb;

    private float distance = 5f;
    private int numOfTrajectoryPoints = 3;

    private List<GameObject> trajectoryPoints;

    private void Start()
    {
        trajectoryPoints = new List<GameObject>();

        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            var point = Instantiate(TrajectoryPointPrefeb);
            var renderer = point.GetComponent<SpriteRenderer>();
            renderer.enabled = false;
            trajectoryPoints.Add(point);
        }
    }

    public void ShowTrajectory(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 currentPosition = fromPosition;

        toPosition = GetPointBetweenPoints(fromPosition, toPosition);

        trajectoryPoints.ForEach(point =>
        {
            currentPosition = Vector3.MoveTowards(currentPosition, toPosition, distance / numOfTrajectoryPoints);

            point.transform.position = currentPosition;
            point.GetComponent<SpriteRenderer>().enabled = true;
        });
    }

    private Vector2 GetPointBetweenPoints(Vector2 p1, Vector2 p2)
    {
        float dist_btw_points = Mathf.Sqrt(Mathf.Pow((p2.x - p1.x), 2) + Mathf.Pow((p2.y - p1.y), 2));

        float distance_ratio = distance / dist_btw_points;
        float x = p1.x + distance_ratio * (p2.x - p1.x);
        float y = p1.y + distance_ratio * (p2.y - p1.y);

        return new Vector2(x, y);
    }

    public void HideTrajectory()
    {
        trajectoryPoints.ForEach(point => point.GetComponent<SpriteRenderer>().enabled = false);
    }
    /*
    private void LaunchDefaultBall()
    {
        defaultBallLaunched = true;
        Destroy(defaultBall.gameObject);

        paddle.GetComponent<Paddle>().enabled = true;

        var ball = LaunchBallFromPaddle();

        float velocity = ball.GetBaseVelocity();

        Vector2 direction = mousePosition - ball.transform.position;

        float velPerUnit = velocity / (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));

        ball.SetVelocityVector(new Vector2(velPerUnit * direction.x, velPerUnit * direction.y));
    }*/
}
