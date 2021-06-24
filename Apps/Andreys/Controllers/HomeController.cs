namespace Andreys.App.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse IndexSlash()
        {
            
            return this.Index();
        }

        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                var obj = new object ();
                this.View(obj, "Home");//подаваме конкретно view!
            }
            return this.Index();
        }
    }
}
