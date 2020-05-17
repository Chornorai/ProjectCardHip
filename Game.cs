﻿
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
            if (mover != ActivePlayer || mover != passivePlayer) return;

            if (mover.PlayerCards.Cards.IndexOf(card) == -1) return;

            //проверка, может ли он класть такую карту
            if(mover==ActivePlayer)
                {}
                ///
            else
                    {
                    //может ли побить
                    }
            Table.Add(mover.PlayerCards.Pull(card));
            //ActivePlayer = NextPlayer(ActivePlayer);
            MarkMover(null);//если активный, то пасивный и наоборот);
            Refresh();
        }

        public void Take()
            {//пассивный игрок забирает карты со стола
            //меняем активного игрока
        }

        public void Otboy()
        {
            //убиаем карты со стола
            //пассивный становится активным, сл. становится пассивным
        }


        public ChekWinner()
            {
            //определяет, есть ли победитель
        }

        public void Attack(CardFigure figure , CardSuit suit , Card card)
        {
            //if (card.Figure == )
            //{
            //    Deck.Card = card;
            //    Player.Cards.Cards.Remove(card);
            //}
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

        public void HangUp()
        {
            Table.Cards.Clear();
            Refresh();
        }
    }
}
