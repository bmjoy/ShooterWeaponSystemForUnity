﻿namespace NebusokuDev.ShooterWeaponSystem.Core.Weapon
{
    public interface IWeaponInput
    {
        public bool IsPrimaryAction { get; }
        public bool IsPrimaryAltAction { get; }
        public bool IsSecondaryAction { get; }
        public bool IsSecondaryAltAction { get; }
        public bool IsReload { get; }
    }
}