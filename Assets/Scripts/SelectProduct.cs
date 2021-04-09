using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectProduct : MonoBehaviour
{
    private Button button;

    private SellingController sellingController;

    public int productIndex;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Select);

        sellingController = GameObject.Find("CashDesk").GetComponent<SellingController>();
    }

    // Update is called once per frame
    void Select()
    {

        sellingController.SelectProduct(productIndex);
    }
}
