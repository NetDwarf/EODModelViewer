using System;

namespace EODModelViewer.Models
{
    public class Item : IModelObject
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsArmor { get; set; }
        public bool IsWeapon { get; set; }
        public int WeaponHand { get; set; }
        public WeaponHands WeaponHandName { get; set; }
        public bool IsSiegeWeapon { get; set; }
        public bool IsHousingItem { get; set; }
        public bool IsWorldObject { get; set; }
        public bool IsInventory { get; set; }
        public bool IsOther { get; set; }

        public enum WeaponHands
        {
            LeftHanded = 0,
            TwoHanded = 1,
            RightHand = 2
        }
    }
}