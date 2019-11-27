﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMember : Member
{
    public float task_time = 3.0f;

    override public void GenerateInfo()
    {
        info.name = "Magox";
        info.lvl = 1;
        info.xp = 0;
        info.type = MEMBER_TYPE.MAGE;
    }
    protected override void ChangeState(uint hour)
    {
        if (state == MEMBER_STATE.QUEST)
            return;

        switch (hour)
        {
            case 6:
                state = MEMBER_STATE.WORK;
                // go tavern
                break;
            case 9:
                // go blacksmith
                break;
            case 13:
                state = MEMBER_STATE.SLEEP;
                // go tabern
                break;
            case 15:
                // go train
                break;
            case 19:
                state = MEMBER_STATE.WORK;
                // go blacksmith
                break;
            case 21:
                //go tabern
                break;
            case 23:
                //go sleep
                break;
        } 
    }

    override protected string GetMemberWorkString()
    {
        return "Making Potions";
    }
}
