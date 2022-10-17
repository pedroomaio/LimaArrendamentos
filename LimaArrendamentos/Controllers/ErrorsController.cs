namespace LimaArrendamentos.Controllers
{
    using System.Diagnostics;
    using LimaArrendamentos.Models;
    using Microsoft.AspNetCore.Mvc;


    /// <summary>
    /// Defines the <see cref="ErrorsController" />.
    /// </summary>
    public class ErrorsController : Controller
    {
        /// <summary>
        /// The Error.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// The Error404.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
