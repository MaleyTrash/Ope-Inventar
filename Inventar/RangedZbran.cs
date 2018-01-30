namespace Inventar
{
    class RangedZbran : Zbran
    {
        public RangedZbran(string name, int damage, int range) : base(name, damage, range)
        {
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
