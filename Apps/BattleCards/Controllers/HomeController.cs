namespace BattleCards.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse Index()
        {
            //if (this.IsUserSignedIn())
            //{
            //    this.Redirect("Cards/All");
            //}
            return this.View();
        }
    }
}