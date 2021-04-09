using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confirm : MonoBehaviour
{
    private Button button;

    private SellingController sellingController;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(GoCheck);

        sellingController = GameObject.Find("CashDesk").GetComponent<SellingController>();
    }

    // Update is called once per frame
    void GoCheck()
    {

        sellingController.ConfirmSell();
    }
}
