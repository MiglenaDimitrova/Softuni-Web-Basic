using BattleCards.Services;
using BattleCards.ViewModels.Cards;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Controllers
{
    public class CardsController: Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCardInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (String.IsNullOrWhiteSpace(input.Name) || input.Name.Length < 5 || input.Name.Length > 15)
            {
                return this.Error("Name must be between 5 and 15 characters long!");
            }
            if (String.IsNullOrWhiteSpace(input.Image))
            {
                return this.Error("Image Required.");
            }
            if (String.IsNullOrWhiteSpace(input.Keyword))
            {
                return this.Error("Keyword Required.");
            }
            if (String.IsNullOrWhiteSpace(input.Attack)|| !int.TryParse(input.Attack, out _)
                ||int.Parse(input.Attack)<0)
            {
                return this.Error("Attack must be positive number or zero.");
            }
            if (String.IsNullOrWhiteSpace(input.Health) || !int.TryParse(input.Health, out _)
                || int.Parse(input.Health) < 0)
            {
                return this.Error("Attack must be positive number or zero.");
            }
            if (String.IsNullOrWhiteSpace(input.Description) || input.Description.Length > 200)
            {
                return this.Error("Description must be 200 characters at most.");
            }
            var cardId = this.cardsService.Add(input);
            var userId = this.GetUserId();
            this.cardsService.AddToMyCollection(userId, cardId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var allCards = this.cardsService.GetAllCards();
            return this.View(allCards);
        }
        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var myCollection = this.cardsService.GetMyCollection(this.GetUserId());
            return this.View(myCollection);
        }
        public HttpResponse AddToCollection(string cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (this.cardsService.IsCardInMyCollection(this.GetUserId(), cardId))
            {
                return this.Error("This card is already in My Collection.");
            }
            else
            {
                this.cardsService.AddToMyCollection(this.GetUserId(), cardId);
            }
            return this.Redirect("/Cards/All");
        }
        public HttpResponse RemoveFromCollection(string cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            this.cardsService.RemoveFromMyCollection(this.GetUserId(), cardId);
            return this.Redirect("/Cards/Collection");
        }
    }
}
