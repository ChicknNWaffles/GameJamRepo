using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSlideFading : PlatformSlide
{
    public bool fadeOverTime;
    public float holdFade = 1f;
    public float fadeAmount = 0f;
    public float fadeSpeed = 1f;

    public bool mouseDebug = false;

    //bool wasFading = false;
    bool fading = false;
    float fadeEnd;

    Collider2D col;

    SpriteRenderer sprite;
    Color tmp;

    private void OnFaded()
    {
        //col.isTrigger = true;
        col.enabled = false;
        if (player)
        {
            player.transform.SetParent(null);
            player = null;
        }
    }

    private void OnUnfadeStart()
    {

    }

    private void OnUnfadeEnd()
    {

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        sprite = GetComponentInChildren<SpriteRenderer>();
        col = GetComponentInChildren<Collider2D>();
    }

    private void StartFading()
    {
        if (fadeAmount == 1f) return;

        fadeAmount = Mathf.MoveTowards(fadeAmount, 1f, fadeSpeed * Time.fixedDeltaTime);
        fading = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!fadeOverTime)
        {
            if (fading)
            {
                fadeAmount = Mathf.MoveTowards(fadeAmount, 1f, fadeSpeed * Time.deltaTime);

                if (fadeAmount == 1f)
                {
                    fading = false;
                    fadeEnd = Time.time + holdFade;
                    OnFaded();
                }
            }
            else if (Time.time > fadeEnd)
            {
                fadeAmount = Mathf.MoveTowards(fadeAmount, 0f, fadeSpeed * Time.deltaTime);
                col.enabled = true;
                //col.isTrigger = false;
            }
        }
        else
        {
            if (fading && fadeAmount == 1f)
            {
                fading = false;
                fadeEnd = Time.time + holdFade;
                OnFaded();
            }

            if (fading == false && (fadeAmount != 1f || Time.time > fadeEnd))
            { 
                fadeAmount = Mathf.MoveTowards(fadeAmount, 0f, fadeSpeed * Time.deltaTime);
                //col.isTrigger = false;
                col.enabled = true;
            }

            fading = false;
        }

        tmp = sprite.color;
        tmp.a = 1f - fadeAmount;
        sprite.color = tmp;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!fadeOverTime && !fading && collision.gameObject.tag == "Player")
        {
            fading = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        if (fadeOverTime)
        {
            StartFading();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
        if (!fadeOverTime && !fading && collision.gameObject.tag == "Player")
        {
            fading = true;
        }
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.tag != "Player") return;

        if (fadeOverTime)
        {
            StartFading();
        }
    }

    private void OnMouseOver()
    {
        if (!mouseDebug) return;

        

        if (fadeOverTime)
        {
            StartFading();
        }

        fading = true;
    }
}
