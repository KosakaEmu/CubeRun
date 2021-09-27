using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem 
{
    private string shopName;
    private int shopPrice;
    private bool isBuy;
    private bool isUse;


    public string ShopName { get => shopName; set => shopName = value; }
    public int ShopPrice { get => shopPrice; set => shopPrice = value; }
    public bool IsBuy { get => isBuy; set => isBuy = value; }
    public bool IsUse { get => isUse; set => isUse = value; }

    public ShopItem() { }
    public ShopItem(string shopName, int shopPrice,bool isBuy, bool isUse) {
        this.shopName = shopName;
        this.shopPrice = shopPrice;
        this.isBuy = isBuy;
        this.isUse = isUse;
    }
}
