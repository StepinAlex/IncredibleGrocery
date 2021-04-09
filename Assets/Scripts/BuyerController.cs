using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerController : MonoBehaviour
{
    Rigidbody2D buyerRB;
    Animator anim;

    float timer;
    public float changeTime = 5.0f;
    public float speed = 0.5f;
    int direction = 1;
    Vector2 pos;

    public GameObject emoCloud;
    public GameObject good;
    public GameObject bad;

    bool buying = false;
    bool bought = false;

    void Start()
    {
        buyerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        timer = changeTime;
    }

    void Update()
    {

        if (buying & timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {

        }


    }

    void FixedUpdate()
    {
        pos = buyerRB.position;
        Vector2 position = buyerRB.position;

        if (!buying)
        {
            position.x = position.x + 4 * speed * Time.deltaTime * direction;
            position.y = position.y - speed * Time.deltaTime * direction;
            anim.SetBool("isWalk", true);
            anim.SetBool("Back", false);
        }

        else if (bought) //pos.x > -8.0f
        {

            position.x = position.x + 4 * speed * Time.deltaTime * direction;
            position.y = position.y - speed * Time.deltaTime * direction;

            anim.SetBool("isWalk", true);
            anim.SetBool("Back", true);
        }


        buyerRB.MovePosition(position);
    }


    public void StartBuying()
    {
        buying = true;
        anim.SetBool("isWalk", false);
        anim.SetBool("Back", false);
    }


    public void HaveProducts(bool satisfaction)
    {
        emoCloud.gameObject.SetActive(true);

        if (satisfaction == true)
        {
            good.gameObject.SetActive(true);
        }
        else
        {
            bad.gameObject.SetActive(true);
        }

        direction = -direction;
        bought = true;
        anim.SetBool("Back", true);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Entrance"))
        {
            Destroy(gameObject);
        }

    }
}
