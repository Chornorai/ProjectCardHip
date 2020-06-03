
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameWF
{
    class Game
    {
        public CardSet Table { get; }
        public CardSet Otboy { get; set; }
        public List<Player> Players { get; }
        public CardSet Deck { get; }
        public Player ActivePlayer { get; set; }
        public Player PassivePlayer { get; set; }
        public Card Trump { get; set; } 

        public Player Mover { get; set; }

        public Action<Player> MarkMover;
        public Action<string> ShowMessage;

        public Game(CardSet table, CardSet deck, params Player[] players)
        {
            Otboy = new CardSet();
            Table = table;
            Players = new List<Player>(players);
            Deck = deck;
        }

        public void Move(Player mover, Card card)
        {
            if (Table.Cards.Count >= 2) return;

            if (mover != ActivePlayer && mover != PassivePlayer) return;

            if (mover.PlayerCards.Cards.IndexOf(card) == -1) return;

            if (mover == ActivePlayer && Table.Cards.Count != 0) return;

            if (mover == PassivePlayer)
            {
                if (Table.Cards.Count == 0) return;

                if (!IsBeat(card, Table.Cards[0])) return;
            }

            Table.Add(mover.PlayerCards.Pull(card));

            Mover = Mover == ActivePlayer ? PassivePlayer : ActivePlayer;
            MarkMover(Mover);

            Refresh();
        }

        private bool IsBeat(Card movingCard, Card tableCard)
        {
            if (movingCard.Suit != Trump.Suit && movingCard.Suit != tableCard.Suit)
                return false;

            if (movingCard.Suit == Trump.Suit && tableCard.Suit != Trump.Suit) return true;

            if (movingCard.Figure > tableCard.Figure) return true;

            return false;
        }

        public void Take()
        {
          
            PassivePlayer.PlayerCards.Add(Table.Deal(Table.Cards.Count));
            Dobor();
            ActivePlayer = NextPlayer(PassivePlayer);
            PassivePlayer = NextPlayer(ActivePlayer);
            Mover = ActivePlayer;
            MarkMover(Mover);
            Refresh();
        }

        public void ToOtboy()
        {
            Otboy.Add(Table.Deal(Table.Cards.Count));
            Dobor();
            ActivePlayer = PassivePlayer;
            PassivePlayer = NextPlayer(ActivePlayer);
            Mover = ActivePlayer;
            MarkMover(Mover);
            ShowMessage("Otbilsa");
            Refresh();
        }

        private void Dobor()
        {
            foreach (var item in Players)
            {
                while (item.PlayerCards.Cards.Count < 3)
                {
                    if (Deck.Cards.Count < 3 - item.PlayerCards.Cards.Count)
                    {
                        if (Deck.Cards.Count == 0)
                        {
                            item.PlayerCards.Add(Trump);
                            Deck.Add(Otboy.Deal(Otboy.Cards.Count));
                            Deck.Mix();
                        }
                        else
                            item.PlayerCards.Add(Deck.Deal(Deck.Cards.Count));
                    }
                    else
                    {
                        item.PlayerCards.Add(Deck.Deal(3 - item.PlayerCards.Cards.Count));
                    }
                }
            }
        }

        public void CheсkWinner()
        {
            foreach (var player in Players)
            {
                if (IsPlayerWin(player))
                    ShowMessage($"Player {player.Name} win!");

            }
        }

        private bool IsPlayerWin(Player player)
        {
            if (player.PlayerCards.Cards.Count != 3) return false;

            CardSuit suit = player.PlayerCards.Cards[0].Suit;
            foreach (var card in player.PlayerCards.Cards)
            {
                if (card.Suit != suit) return false;
            }
            return true;
        }

        public void Refresh()
        {
            foreach (var item in Players)
            {
                item.PlayerCards.Show();
            }
            Table.Show();
            CheсkWinner();
        }

        private Player NextPlayer(Player player)
        {
            if (player == Players[Players.Count - 1]) return Players[0];

            return Players[Players.IndexOf(player) + 1];
        }

        private Player PreviousPlayer(Player player)
        {
            if (player == Players[0]) return Players[Players.Count - 1];

            return Players[Players.IndexOf(player) - 1];
        }

        public void Deal()
        {
            Deck.Mix();
            Trump = Deck.Deal(1).Cards[0];
            foreach (var item in Players)
            {
                item.PlayerCards.Add(Deck.Deal(3));
            }

            ActivePlayer = Players[0];
            PassivePlayer = NextPlayer(ActivePlayer);
            Mover = ActivePlayer;
            MarkMover(Mover);
            Refresh();
        }
        //public void GameOver()
        //{
        //    foreach (var item in Players)
        //    {
        //        if (item.PlayerCards.Cards.Count != 0)
        //            ShowMessage(item.Name + "loose");
        //    }
        //}
    }
}
