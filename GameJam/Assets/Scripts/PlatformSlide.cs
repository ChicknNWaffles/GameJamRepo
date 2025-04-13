using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlatformSlide : MonoBehaviour
{
    public Transform otherPos;
    public float moveSpeed;
    [Tooltip("Keep this smaller than 1 for responsive movement")]
    public float lerpFactor = .9f;
    public EaseChoice easeChoice = EaseChoice.InOutSine;
    public bool flip;
    public bool flippedOnStart;

    public ContactFilter2D touchFilter;

    Vector3 start;
    Vector3 moveTo;
    float curDist;
    float distBetweenPoints;

    private float startScale;

    protected GameObject player;

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

        startScale = transform.localScale.x;

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

        if (flip && moveTo == start)
        {
            if (flippedOnStart)
            {
                transform.localScale = new Vector2(startScale, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(-startScale, transform.localScale.y);
            }
        }
        else
        {
            if (flippedOnStart)
            {
                transform.localScale = new Vector2(-startScale, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(startScale, transform.localScale.y);
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool onPlatform = collision.gameObject.GetComponent<PlayerPhysics>().IsGroundedTo(GetComponentInChildren<Collider2D>());

            if (onPlatform)
            {
                collision.gameObject.transform.SetParent(transform, true);
                player = collision.gameObject;
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (flip) return;

        if (collision.gameObject.tag == "Player")
        {
            bool onPlatform = collision.gameObject.GetComponent<PlayerFrogger>().grounded;

            if (onPlatform && collision.gameObject.transform.parent != transform)
            {
                collision.gameObject.transform.SetParent(transform, true);
                player = collision.gameObject;
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
            player = null;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (flip) return;

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
            player = null;
        }
    }
}

public enum EaseChoice
{
    InOutSine,
    InOutQuad
}