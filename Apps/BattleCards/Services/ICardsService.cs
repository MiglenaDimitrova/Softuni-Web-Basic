using BattleCards.Models;
using BattleCards.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        string Add(AddCardInputModel input);
        void AddToMyCollection(string userId, string cardId);
        void RemoveFromMyCollection(string userId, string cardId);
        bool IsCardInMyCollection(string userId, string cardId);
        ICollection<CardViewModel> GetAllCards();
        ICollection<CardViewModel> GetMyCollection(string userId);

    }
}
