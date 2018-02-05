using System.Collections.Generic;

namespace Inventar
{
    class MagicItem : Item
    {
        public List<MagicEffect> magicEffects;

        public MagicItem(string name, ItemType type, IEnumerable<MagicEffect> magicEffects) : base(name, type)
        {
            this.magicEffects = new List<MagicEffect>(magicEffects);
        }
    }
}
