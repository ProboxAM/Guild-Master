﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    Move move;
    Animator anim;
    SteeringFollowNavMeshPath steer;
    Collider coll;
    DayNightCicle time;

    public GameObject locations;
    public GameObject model;

    enum CHARACTER_ACTION { NONE, DISAPPEAR, TYPE_ACTION, TALK };
    CHARACTER_ACTION current_action;

    public enum CHARACTER_TYPE {NONE, KNIGHT, HUNTER, MAGE};
    public CHARACTER_TYPE type;

    string type_string;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        move = this.GetComponent<Move>();
        anim = this.GetComponent<Animator>();
        
        time = GameObject.Find("GameManager").GetComponent<DayNightCicle>();

        DayNightCicle.OnHourChange += ChangeAction;
        steer = GetComponent<SteeringFollowNavMeshPath>();
        steer.OnReachEnd += DoAction;


        switch (type)
        {
            case CHARACTER_TYPE.KNIGHT:
                type_string = "Warrior";
                break;
            case CHARACTER_TYPE.HUNTER:
                type_string = "Hunter";
                break;
            case CHARACTER_TYPE.MAGE:
                type_string = "Mage";
                break;
        }
        anim.SetInteger("char_type", (int)type);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", move.current_velocity.magnitude);
    }

    public void DoAction()
    {
        switch (current_action)
        {
            case CHARACTER_ACTION.DISAPPEAR:
                Disappear(true);
                break;
            case CHARACTER_ACTION.TALK:

                break;
            case CHARACTER_ACTION.TYPE_ACTION:
                anim.SetBool("type_action", true);
                break;
            default:
                break;
        }
    }

    void Disappear(bool mode)
    {
        if(mode)
        {
            model.SetActive(false);
            coll.enabled = false;
        }          
        else
        {
            model.SetActive(true);
            coll.enabled = true;
        }
    }

    void ChangeAction()
    {
        switch (time.GetHour())
        {
            case 6:
                steer.CreatePath(locations.transform.Find("Tabern Location").transform.position);
                current_action = CHARACTER_ACTION.DISAPPEAR;
                Disappear(false);
                // go tabern
                break;
            case 8:
                steer.CreatePath(locations.transform.Find("Blacksmith Location").transform.position);
                current_action = CHARACTER_ACTION.TALK;
                Disappear(false);
                // go blacksmith
                break;
            case 10:
                steer.CreatePath(locations.transform.Find(type_string + " Location").transform.position);
                current_action = CHARACTER_ACTION.TYPE_ACTION;
                //go train
                break;
            case 13:
                steer.CreatePath(locations.transform.Find("Tabern Location").transform.position);
                current_action = CHARACTER_ACTION.DISAPPEAR;
                anim.SetBool("type_action", false);
                // go tabern
                break;
            case 16:
                steer.CreatePath(locations.transform.Find(type_string + " Location").transform.position);
                current_action = CHARACTER_ACTION.TYPE_ACTION;
                Disappear(false);
                // go train
                break;
            case 20:
                steer.CreatePath(locations.transform.Find("Tabern Location").transform.position);
                current_action = CHARACTER_ACTION.DISAPPEAR;
                anim.SetBool("type_action", false);
                //go tabern
                break;
            case 23:
                steer.CreatePath(locations.transform.Find("Guild Hall Location").transform.position);
                current_action = CHARACTER_ACTION.DISAPPEAR;
                Disappear(false);
                //go sleep
                break;
        }
    }

}