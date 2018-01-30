namespace Inventar
{
    class MeleeZbran : Zbran
    {
        public MeleeZbran(string name, int damage) : base(name, damage, 2)
        {
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
