using System.Collections.Generic;

namespace Inventar
{
    class MeleeZbran : Zbran
    {
        public MeleeZbran(Item item, int damage) : base(item, damage, 2)
        {
        }

        public new void Attack()
        {
            throw new System.NotImplementedException();
        }
    }

    class WeaponEffect : MagicEffect
    {
        public int extraDamage;
        public WeaponEffect(string name, int extraDamage) : base(name)
        {
            this.extraDamage = extraDamage;
        }
    }

    class Excaliboar : MagicItem, IZbran
    {
        public Excaliboar() : base("Excaliboar", ItemType.Weapon, new WeaponEffect[] { new WeaponEffect("Sharpness", 10) })
        {
            
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
