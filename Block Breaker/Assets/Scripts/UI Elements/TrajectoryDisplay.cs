using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TrajectoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject TrajectoryPointPrefeb;

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

    public void ShowTrajectory(Vector3 fromPosition, Vector3 toPosition, float distance)
    {
        Vector3 currentPosition = fromPosition;

        toPosition = Vector3.MoveTowards(fromPosition, toPosition, 5);

        trajectoryPoints.ForEach(point =>
        {
            currentPosition = Vector3.MoveTowards(currentPosition, toPosition, distance / numOfTrajectoryPoints);

            point.transform.position = currentPosition;
            point.GetComponent<SpriteRenderer>().enabled = true;
        });
    }

    void SetTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
        fTime += 0.1f;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + i, pStartPosition.y + i, 2); // The value 2 here is replaced with transform.position.z when it doesn't work
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].GetComponent<SpriteRenderer>().enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }

}
