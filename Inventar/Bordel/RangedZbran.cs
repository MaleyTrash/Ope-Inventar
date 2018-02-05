namespace Inventar
{
    class RangedZbran : Zbran
    {
        public RangedZbran(Item item, int damage, int range) : base(item, damage, range)
        {
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
