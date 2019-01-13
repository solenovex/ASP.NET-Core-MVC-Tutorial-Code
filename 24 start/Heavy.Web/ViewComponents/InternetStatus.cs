using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Heavy.Web.ViewComponents
{
    public class InternetStatus: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://www.baidu.com");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return View(true);
            }

            return View(false);
        }
    }
}
