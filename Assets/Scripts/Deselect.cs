using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deselect : MonoBehaviour
{
    private Button button;

    private SellingController sellingController;

    public int selectNumber;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ClearButton);

        sellingController = GameObject.Find("CashDesk").GetComponent<SellingController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ClearButton()
    {
        
        sellingController.ClearSelect(selectNumber);
    }


}
