using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.Audio;

public class SellingController : MonoBehaviour
{
    int amount;
    int productIndex;
    int select = 0;
    int check = 0;
    int orderMoney = 0;

    private AudioSource playAudio;
    public float soundsVol;

    //Sound FX
    public AudioClip appearSound; // звук при появлении облачка над персонажей.
    public AudioClip disappearSound; //звук при пропадании облачка над персонажами.
    public AudioClip clickSound; // звук нажатия на кнопки.
    public AudioClip selectSound; //звук выбора продукта в инвентаре.
    public AudioClip moneySound; //звук начисления денег за заказ.
    

    // Poduct Arrays
    public GameObject[] productsPrefab;
    public GameObject[] selectedProducts;

    // Store elements
    public GameObject storePanel;
    public GameObject[] selectButtons;
    public GameObject select1;
    public GameObject select2;
    public GameObject select3;
    public GameObject sellButton;
    public GameObject sellMask;

    // Clouds elements
    public GameObject buyCloud;
    public GameObject buySlot1;
    public GameObject buySlot2;
    public GameObject buySlot3;


    public GameObject sellCloud;
    public GameObject cloudMask;
    public GameObject sellSlot1;
    public GameObject sellSlot2;
    public GameObject sellSlot3;
    public GameObject[] okIcons;
    public GameObject[] noIcons;

    private GameManager gameManager;


    int firstWant;
    int secondWant;
    int thirdWant;
    int firstSelect;
    int secondSelect;
    int thirdSelect;

    bool generated = false;
    bool checking = false;
    bool slideLeft = false;
    bool slideRight = false;
    bool sel1 = false;
    bool sel2 = false;
    bool sel3 = false;
    bool firstRight = false;
    bool secondRight = false;
    bool thirdRight = false;


    float timer;
    public float changeTime = 3.0f;
    float splitTimer;
    float halfsecond = 0.5f;

    public float storeZone = 1750;
    public float speed = 500f;

    void Start()
    {
        playAudio = GetComponent<AudioSource>();
        timer = changeTime;
        splitTimer = halfsecond;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (generated && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (generated && timer < 0)
        {
            generated = false;
            ShowPanel();
            timer = changeTime;
        }

        if (checking && splitTimer >0)
        {
            splitTimer -= Time.deltaTime;
        }
        else if (checking && splitTimer <= 0)
        {
            splitTimer = halfsecond;
            CheckSelection();
        }

        if (slideLeft == true && storePanel.transform.position.x > storeZone)
        {
            storePanel.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (!slideRight && storePanel.transform.position.x <= storeZone)
        {
            slideLeft = false;
        }
        else if (slideRight && storePanel.transform.position.x <= storeZone + 1000f)
        {
            storePanel.transform.position -= Vector3.left * speed * Time.deltaTime;
        }
        else if (slideRight && storePanel.transform.position.x >= 1250f)
        {
            slideRight = false;
            ShowSelection();
        }

    }


    int RandomAmount()
    {
        amount = Random.Range(1, 4);
        return amount;
    }


    public void GenerateProducts()
    {

        RandomAmount();

        int selected = 0;
        int[] wantProducts = new int[amount];
        Debug.Log("Want " + amount + " products");

        do
        {
            wantProducts[selected] = Random.Range(0, productsPrefab.Length - 1);

                 if (selected == 0 )
                {
                    firstWant = wantProducts[selected];
                    productsPrefab[firstWant].gameObject.SetActive(true);
                    productsPrefab[firstWant].gameObject.transform.position = buySlot1.transform.position;
                    selected = selected + 1;
                }
                else if (selected == 1 && (wantProducts[selected] != wantProducts[selected - 1]))
                {
                    secondWant = wantProducts[selected];
                    productsPrefab[secondWant].gameObject.SetActive(true);
                    productsPrefab[secondWant].gameObject.transform.position = buySlot2.transform.position;
                    selected = selected + 1;
                }
                else if (selected == 2 && (wantProducts[selected] != wantProducts[selected - 1]) 
                    && (wantProducts[selected] != wantProducts[selected - 2]))
                {
                    thirdWant = wantProducts[selected];
                    productsPrefab[thirdWant].gameObject.SetActive(true);
                    productsPrefab[thirdWant].gameObject.transform.position = buySlot3.transform.position;
                    selected = selected + 1;
                }
                //Debug.Log("Selected product is: " + productsPrefab[wantProducts[selected]].gameObject.name);
                
        }
        while (selected != amount);


        buyCloud.gameObject.SetActive(true);
        buySlot1.gameObject.SetActive(true);
        buySlot2.gameObject.SetActive(true);
        buySlot3.gameObject.SetActive(true);
        playAudio.PlayOneShot(appearSound, soundsVol);

        generated = true;
        select = 0;
    }


    void ShowPanel()
    {

        buySlot1.gameObject.SetActive(false);
        buySlot2.gameObject.SetActive(false);
        buySlot3.gameObject.SetActive(false);
        buyCloud.gameObject.SetActive(false);
        productsPrefab[firstWant].gameObject.SetActive(false);
        productsPrefab[secondWant].gameObject.SetActive(false);
        productsPrefab[thirdWant].gameObject.SetActive(false);
        playAudio.PlayOneShot(disappearSound, soundsVol); //звук при пропадании облачка над персонажами.

        storePanel.gameObject.SetActive(true);
        slideLeft = true;

        select = 0;
        select1.gameObject.SetActive(false);
        select2.gameObject.SetActive(false);
        select3.gameObject.SetActive(false);
        sel1 = false;
        sel2 = false;
        sel3 = false;
        selectButtons[firstSelect].gameObject.SetActive(true);
        selectButtons[secondSelect].gameObject.SetActive(true);
        selectButtons[thirdSelect].gameObject.SetActive(true);
        sellButton.gameObject.SetActive(false);
        sellMask.gameObject.SetActive(true);
    }


    public void SelectProduct(int SelectedProduct)
    {

        if (select < amount)
        {
            if (sel1 == false )
            {
                select1.gameObject.SetActive(true);
                select1.gameObject.transform.position = selectButtons[SelectedProduct].transform.position;
                firstSelect = SelectedProduct;
                sel1 = true;

            }
            else if (sel2 == false )
            {
                select2.gameObject.SetActive(true);
                select2.gameObject.transform.position = selectButtons[SelectedProduct].transform.position;
                secondSelect = SelectedProduct;
                sel2 = true;
            }
            else if (sel3 == false )
            {
                select3.gameObject.SetActive(true);
                select3.gameObject.transform.position = selectButtons[SelectedProduct].transform.position;
                thirdSelect = SelectedProduct;
                sel3 = true;
            }
            selectButtons[SelectedProduct].gameObject.SetActive(false);
            select = select + 1;
            Debug.Log("Product selected " + selectButtons[SelectedProduct].name);
            playAudio.PlayOneShot(selectSound, soundsVol); //звук выбора продукта в инвентаре.
        }
        CheckSell();
    }

    public void ClearSelect(int selectedNumber)
    {

        int prevSelect = 0;

        if (selectedNumber == 1)
        {
            sel1 = false;
            select = select - 1;
            select1.gameObject.SetActive(false);
            prevSelect = firstSelect;
        }
        else if (selectedNumber == 2)
        {
            sel2 = false;
            select = select - 1;
            select2.gameObject.SetActive(false);
            prevSelect = secondSelect;
        }
        else if (selectedNumber == 3)
        {
            sel3 = false;
            select = select - 1;
            select3.gameObject.SetActive(false);
            prevSelect = thirdSelect;
        }

        selectButtons[prevSelect].gameObject.SetActive(true);

        Debug.Log("selecting " + selectedNumber + " is cleared");
        CheckSell();
        
        
    }

    void CheckSell()
    {
        if (select == amount)
        {
            sellMask.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(true);
        }
        else
        {
            sellButton.gameObject.SetActive(false);
            sellMask.gameObject.SetActive(true);
        }
        playAudio.PlayOneShot(clickSound, soundsVol); 
    }

    public void ConfirmSell()
    {
        slideRight = true;
        playAudio.PlayOneShot(clickSound, soundsVol); 
    }

    public void ShowSelection()
    {
        
        cloudMask.gameObject.SetActive(false);
        productsPrefab[firstSelect].gameObject.SetActive(true);
        productsPrefab[firstSelect].gameObject.transform.position = sellSlot1.transform.position;
        sellSlot1.gameObject.SetActive(true);
        playAudio.PlayOneShot(clickSound, 1.0f);
        Debug.Log(productsPrefab[firstSelect].gameObject.name);

        if (amount >= 2)
        {
            productsPrefab[secondSelect].gameObject.SetActive(true);
            productsPrefab[secondSelect].gameObject.transform.position = sellSlot2.transform.position;
            sellSlot2.gameObject.SetActive(true);
        }
        if (amount == 3)
        {
            sellSlot3.gameObject.SetActive(true);
            productsPrefab[thirdSelect].gameObject.SetActive(true);
            productsPrefab[thirdSelect].gameObject.transform.position = sellSlot3.transform.position;
        }


        sellCloud.gameObject.SetActive(true);
        firstRight = false;
        secondRight = false;
        thirdRight = false;
        check = 0;
        checking = true;

        for(int i = 0; i < 3; i++)
        {
            okIcons[i].gameObject.SetActive(false);
            noIcons[i].gameObject.SetActive(false);
        }
        
    }

    public void CheckSelection()
    {
        cloudMask.gameObject.SetActive(true);
        

        if (check == 0)
        {
             if (firstSelect == firstWant || firstSelect == secondWant || firstSelect == thirdWant)
             {
                okIcons[check].gameObject.SetActive(true);
                firstRight = true;
                orderMoney = orderMoney + 10;
            }
             else
             {
                noIcons[check].gameObject.SetActive(true);
                firstRight = false;
                
            }
        }

        if (amount> 1 && check == 1)
        {
            if (secondSelect == firstWant || secondSelect == secondWant || secondSelect == thirdWant)
            {
                okIcons[check].gameObject.SetActive(true);
                secondRight = true;
                orderMoney = orderMoney + 10;
            }
            else
            {
                noIcons[check].gameObject.SetActive(true);
                secondRight = false;
                
            }
        }
        else if (amount < 2)
        {
            secondRight = true;
        }


        if (check == 2)
        {
            if (thirdSelect == firstWant || thirdSelect == secondWant || thirdSelect == thirdWant)
            {
                okIcons[check].gameObject.SetActive(true);
                thirdRight = true;
                orderMoney = orderMoney + 10;
            }
            else
            {
                noIcons[check].gameObject.SetActive(true);
                thirdRight = false;
            }
        }
        else if (amount < 3)
        {
            thirdRight = true;
        }

        check = check + 1;

        if (check == amount)
        {
           checking = false;
           CheckResults();
        }

    }

    public void CheckResults()
    {
        BuyerController buyer = FindObjectOfType<BuyerController>();
        
        bool allCorrect = false;

        if(!firstRight || !secondRight || !thirdRight)
        {
            allCorrect = false;
        }
        else
        {
            allCorrect = true;
        }
        if (allCorrect == true)
        {
            orderMoney = orderMoney * 2 ;
        }

        buyer.HaveProducts(allCorrect);
        

        StartCoroutine(Endbuy());
    }

    IEnumerator Endbuy()
    {
        
        if (orderMoney > 0)
        {
            gameManager.UpdateScore(orderMoney);
            playAudio.PlayOneShot(moneySound, soundsVol);
        }

        yield return new WaitForSeconds(1);
        sellCloud.gameObject.SetActive(false);
        cloudMask.gameObject.SetActive(false);
        playAudio.PlayOneShot(disappearSound, soundsVol); //звук при пропадании облачка над персонажами.

        productsPrefab[firstSelect].gameObject.SetActive(false);
        productsPrefab[secondSelect].gameObject.SetActive(false);
        productsPrefab[thirdSelect].gameObject.SetActive(false);
        orderMoney = 0;
    }

    public void SetVolume(float val)
    {
        GetComponent<AudioSource>().volume = val;
        soundsVol = val;
        Debug.Log("soundsVol set " + soundsVol);
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        BuyerController custemer = other.GetComponent<BuyerController>();

        if (custemer != null)
        {
            custemer.StartBuying();
            GenerateProducts();
        }
    }
}
