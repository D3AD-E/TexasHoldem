using System;

namespace TexasHoldemServer.Utils
{
    public class Phrases
    {
        private readonly string[] _Phrases = { "Two seven off-suit is the best hand ever!",
        "Bet, raise, re-raise, all-in!",
        "Gimme gimme gimme money!",
        "Everything you do at the poker table... conveys information.",
        "You can't be all loosey goosey, eating a sandwich.",
        "Money isn’t everything unless you’re playing a rebuy tournament.",
        "If there weren’t luck involved, I would win every time.",
        "Poker is 100% skill and 50% luck.",
        "They say poker is a zero-sum game. It must be, because every time I play my sum ends up zero.",
        "I don’t play any two suited cards. I play any two non-suited cards. That way I’m drawing at two different flushes." };

        public string GetRandomString()
        {
            var rand = new Random();
            return _Phrases[rand.Next(_Phrases.Length)];
        }
    }
}