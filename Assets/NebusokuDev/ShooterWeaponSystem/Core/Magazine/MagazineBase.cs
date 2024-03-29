﻿using System.Collections;
using NebusokuDev.ShooterWeaponSystem.Core.AmmoHolder;
using UnityEngine.Events;


namespace NebusokuDev.ShooterWeaponSystem.Core.Magazine
{
    public abstract class MagazineBase : IMagazine
    {
        public UnityEvent<uint> onReamingChange;
        
        public abstract IAmmoHolder AmmoHolder { get; set; }
        public abstract uint Capacity { get; }
        public abstract uint Reaming { get; }
        public abstract bool UseAmmo(uint useAmount);
        public abstract bool IsReloading { get; }
        public abstract IEnumerator Reload();
    }
}