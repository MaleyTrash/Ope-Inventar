namespace Inventar
{
    abstract class Zbran : Item
    {
        int damage;
        int range;
        public Zbran(string name, int damage, int range) : base(name, ItemType.Weapon)
        {
            this.damage = damage;
        }

        public abstract void Attack();
    }
}
