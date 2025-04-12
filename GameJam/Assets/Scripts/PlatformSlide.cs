using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSlide : MonoBehaviour
{
    public Transform otherPos;
    public float moveSpeed;
    [Tooltip("Keep this smaller than 1 for responsive movement")]
    public float lerpFactor = .9f;
    public EaseChoice easeChoice = EaseChoice.InOutSine;

    Vector3 start;
    Vector3 moveTo;
    float curDist;
    float distBetweenPoints;

    float easeInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1f) / 2f;
    }

    float easeInOutQuad(float x)
    {
        return x < 0.5f ? 2f * x * x : 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;
    }

    float ease(float x)
    {
        switch (easeChoice)
        {
            case EaseChoice.InOutSine:
                return easeInOutSine(x);
            case EaseChoice.InOutQuad:
                return easeInOutQuad(x);
            default:
                return x;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (otherPos == null) otherPos = transform;

        start = transform.position;
        distBetweenPoints = Vector3.Distance(start, otherPos.position);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (moveTo != start) moveTo = otherPos.position;

        curDist = Vector3.Distance(transform.position, moveTo);

        transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime * ease((distBetweenPoints - lerpFactor*curDist) / distBetweenPoints));//Mathf.Lerp(lerpFactor, 1f, curDist/distBetweenPoints));

        if (curDist < .01f) moveTo = moveTo == start ? otherPos.position : start;
    }
}

public enum EaseChoice
{
    InOutSine,
    InOutQuad
}