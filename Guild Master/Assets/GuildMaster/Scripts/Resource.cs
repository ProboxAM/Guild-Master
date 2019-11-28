﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public enum ResourceType
    {
        Gold,
        Crown,
        Shield,
        Potion,
        Meat,
    }

    private ResourceType type;
    private uint amount;

    public uint GetAmount()
    {
        return amount;
    }
    public ResourceType GetResourceType()
    {
        return type;
    }

    public Resource(ResourceType type, uint amount)
    {
        this.type = type;
        this.amount = amount;
    }

    internal void Decrease(uint v)
    {
        amount -= v;
        if (amount < 0)
            amount = 0;
    }

    internal void Increase(uint v)
    {
        amount += v;
    }
}