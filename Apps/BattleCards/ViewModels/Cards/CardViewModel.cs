using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.ViewModels.Cards
{
    public class CardViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Attack { get; set; }
        public string Health { get; set; }

    }
}
