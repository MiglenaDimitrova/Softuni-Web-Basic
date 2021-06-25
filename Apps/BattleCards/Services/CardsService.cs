using BattleCards.Data;
using BattleCards.Models;
using BattleCards.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string Add(AddCardInputModel input)
        {
            var card = new Card
            {
                Name = input.Name,
                Keyword = input.Keyword,
                ImageUrl = input.Image,
                Attack = int.Parse(input.Attack),
                Health = int.Parse(input.Health),
                Description = input.Description
            };
            this.db.Cards.Add(card);
            this.db.SaveChanges();
            return card.Id;
        }

        public void AddToMyCollection(string userId, string cardId)
        {
            var userCard = new UserCard
            {
                UserId = userId,
                CardId = cardId
            };
            this.db.UsersCards.Add(userCard);
            this.db.SaveChanges();
        }

        public ICollection<CardViewModel> GetAllCards()
        {
            return this.db.Cards
                .Select(x => new CardViewModel
                {
                     Id = x.Id,
                     Name = x.Name,
                     Keyword = x.Keyword,
                     Attack = x.Attack.ToString(),
                     Description = x.Description,
                     Health = x.Health.ToString(),
                     ImageUrl = x.ImageUrl
                }).ToList();
        }

        public ICollection<CardViewModel> GetMyCollection(string userId)
        {
            return this.db.UsersCards
               .Where(x=>x.UserId==userId)
               .Select(x => new CardViewModel
               {
                   Id = x.Card.Id,
                   Name = x.Card.Name,
                   Keyword = x.Card.Keyword,
                   Attack = x.Card.Attack.ToString(),
                   Description = x.Card.Description,
                   Health = x.Card.Health.ToString(),
                   ImageUrl = x.Card.ImageUrl
               }).ToList();
        }

        public bool IsCardInMyCollection(string userId, string cardId)
        {
            var card = this.db.UsersCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);
            return card == null ? false : true;
        }

        public void RemoveFromMyCollection(string userId, string cardId)
        {
            var card = this.db.UsersCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);
            this.db.UsersCards.Remove(card);
            this.db.SaveChanges();
        }
    }
}
