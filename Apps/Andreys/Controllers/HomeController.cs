namespace Andreys.App.Controllers
{
    using Andreys.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!this.IsUserSignedIn())
            {
                return this.View();
            }
            else
            {
                return this.Home();
            }
            
        }
        public HttpResponse Home()
        {
                //TODO : Validations
                var products = this.productsService.GetAllProducts();
                return this.View(products);
        }
    }
}
