
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
        public List<Player> Players { get; }
        public CardSet Deck { get; }
        public Player ActivePlayer { get; set; }
        public Player PassivePlayer { get; set; }

        public Action<Player> MarkMover;
        public Action<string> ShowMessage;

        public Game(CardSet table, CardSet deck, params Player[] players)
        {
            Table = table;
            Players = new List<Player>(players);
            Deck = deck;
            ActivePlayer = players[0];
        }

        public void Move(Player mover, Card card)
        {
            if (mover != ActivePlayer || mover != PassivePlayer) return;

            if (mover.PlayerCards.Cards.IndexOf(card) == -1) return;

           
            if(mover==ActivePlayer)
            {
            }
            
            else
            {
                 
            }
            Table.Add(mover.PlayerCards.Pull(card));
            ActivePlayer = NextPlayer(ActivePlayer);
            MarkMover(null);
            Refresh();
        }

        public void Take()
        {
            //пассивный игрок забирает карты со стола
            if ( )
            {

            }
            ActivePlayer = NextPlayer(ActivePlayer);
        }

        public void Otboy()
        {
            Table.Cards.Clear();
            Refresh();
            ActivePlayer = NextPlayer(ActivePlayer);
        }


        public void CheсkWinner()
        {
            //определяет, есть ли победитель
        }
        public void Refresh()
        {
            foreach (var item in Players)
            {
                item.PlayerCards.Show();
            }
            Table.Show();
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
            foreach (var item in Players)
            {
                item.PlayerCards.Add(Deck.Deal(3));
            }
            Refresh();
        }
        public void GameOver()
        {
            foreach (var item in Players)
            {
                if (item.PlayerCards.Cards.Count != 0)
                    ShowMessage(item.Name + "loose");
            }
        }
    }
}
