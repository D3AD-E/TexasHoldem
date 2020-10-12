using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldemCommonAssembly.Game.Utils
{
    internal class CardComparer : IEqualityComparer<Card>
    {
        public bool Equals([AllowNull] Card x, [AllowNull] Card y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode([DisallowNull] Card obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}