namespace Inventar
{
    abstract class Zbran : Item, IZbran
    {
        int damage;
        int range;
        public Zbran(Item item, int damage, int range) : base(item.name, ItemType.Weapon)
        {
            this.damage = damage;
            this.range = range;
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }
    }

    interface IZbran
    {
        void Attack();
    }
}
