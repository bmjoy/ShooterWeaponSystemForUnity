﻿using System;
using NebusokuDev.ShooterWeaponSystem.Core.Collision;
using UnityEngine;


namespace NebusokuDev.ShooterWeaponSystem.Sample.Script
{
    public class SimpleObjectGroup : MonoBehaviour, IObjectGroup
    {
        [SerializeField] private int groupId;
        
        public Guid SelfId => Guid.NewGuid();

        public int GroupId => groupId;
    }
}