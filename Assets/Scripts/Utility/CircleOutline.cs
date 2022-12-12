using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOutline : MonoBehaviour
{
    [SerializeField] LineRenderer circleRend;
    [SerializeField] int steps;

    Vector3[] initialPoints;
    private void Start()
    {
        float radius = GetComponent<CircleCollider2D>().radius;
        DrawCircle(steps, radius);
    }

    private void Update()
    {
        FollowObj();
    }

    private void FollowObj()
    {
        for(int i = 0; i < circleRend.positionCount; i++)
        {
            Vector3 currentPos = initialPoints[i];
            Vector3 newPos = new(transform.parent.position.x + currentPos.x, transform.parent.position.y + currentPos.y,0f);
            circleRend.SetPosition(i, newPos);
        }
    }
    private void DrawCircle(int steps, float radius)
    {
        circleRend.positionCount = steps;

        for(int cStep = 0; cStep < steps; cStep++)
        {
            float circumferenceProgress = (float)cStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);


            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPos = new(x, y, 0f);

            circleRend.SetPosition(cStep, currentPos);
        }


        initialPoints = new Vector3[circleRend.positionCount];
        circleRend.GetPositions(initialPoints);

    }
}
