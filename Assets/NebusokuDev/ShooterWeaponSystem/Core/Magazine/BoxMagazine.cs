﻿using System;
using System.Collections;
using NebusokuDev.ShooterWeaponSystem.Core.AmmoHolder;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace NebusokuDev.ShooterWeaponSystem.Core.Magazine
{
    [Serializable, AddTypeMenu("Box")]
    public class BoxMagazine : MagazineBase
    {
        [SerializeField] private float reloadTime = .5f;
        [SerializeField] private float tacticalReloadTime = .3f;
        [SerializeField] private uint capacity = 30;
        [SerializeField, ReadOnly] private uint reaming = 999;
        [SerializeField] private bool isClosedBolt;

        public BoxMagazine() { }


        public BoxMagazine(uint capacity, uint reaming, bool isClosedBolt, IAmmoHolder ammoHolder)
        {
            this.capacity = capacity;
            this.reaming = reaming;
            this.isClosedBolt = isClosedBolt;
            AmmoHolder = ammoHolder;
        }


        public UnityEvent onTacticalReloadStart;
        public UnityEvent onTacticalReloadEnd;
        public UnityEvent onEmptyReloadStart;
        public UnityEvent onEmptyReloadEnd;

        private WaitForSeconds _tacticalReload;
        private WaitForSeconds _reload;

        public override IAmmoHolder AmmoHolder { get; set; }

        public override uint Capacity => capacity;
        public override uint Reaming => reaming;


        public override bool UseAmmo(uint useAmount)
        {
            reaming = (uint)Mathf.Clamp(reaming, 0, capacity + (isClosedBolt ? 1 : 0));
            useAmount = (uint)Mathf.Clamp(useAmount, 0, Int32.MaxValue);
            reaming = (uint)Mathf.Clamp(reaming, 0, capacity);

            if (useAmount > Reaming) return false;

            reaming -= useAmount;

            return true;
        }


        public override bool IsReloading => _isReloading;
        private bool _isReloading;


        public override IEnumerator Reload()
        {
            reaming = (uint)Mathf.Clamp(reaming, 0, capacity + (isClosedBolt ? 1 : 0));
            var reloadAmount = capacity - reaming;

            if (AmmoHolder.IsEmpty) yield break;
            if (reaming >= capacity) yield break;

            _isReloading = true;

            if (Reaming > 0)
            {
                onTacticalReloadStart.Invoke();

                yield return _tacticalReload ??= new WaitForSeconds(tacticalReloadTime);

                onTacticalReloadEnd.Invoke();
                reaming += AmmoHolder.GetAmmo(reloadAmount) + (uint)(isClosedBolt ? 1 : 0);
            }
            else
            {
                onEmptyReloadStart.Invoke();

                yield return _reload ??= new WaitForSeconds(reloadTime);

                reaming += AmmoHolder.GetAmmo(reloadAmount);
                onEmptyReloadEnd.Invoke();
            }

            _isReloading = false;
        }
    }
}