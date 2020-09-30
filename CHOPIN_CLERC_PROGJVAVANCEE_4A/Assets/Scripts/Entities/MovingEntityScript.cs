﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntityScript : MonoBehaviour
{
    protected Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    [SerializeField]
    protected float speed;

    public abstract void Move();
    public abstract void Freeze();
}
