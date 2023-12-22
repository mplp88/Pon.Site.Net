using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pon.Site.Net.Web.Models;
using Pon.Site.Net.Web.Services;

namespace Pon.Site.Net.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly IToDoService _service;

        public TodoController(IToDoService service)
        {
            _service = service;
        }

        // GET: TodoController
        public async Task<ActionResult> Index()
        {
            try
            {
                var items = await _service.Get();
                return View(items);
            }
            catch(Exception)
            {
                throw;
            }
        }

        // GET: TodoController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var item = await _service.Get(id);
            return View(item);
        }

        // GET: TodoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item item)
        {
            try
            {
                await _service.Add(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            await Task.Delay(1500);
            return View();
        }

        // POST: TodoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, IFormCollection collection)
        {
            try
            {
                await Task.Delay(1500);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _service.Get(id);
            return View(item);
        }

        // POST: TodoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DoDelete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewData["true"] = true;
                ViewData["Message"] = "Ocurrió un error intentando eliminar el registro. Por favor contacte a soporte.";
                ViewData["Exception"] = ex.Message;
                var item = _service.Get(id);
                return View(item);
            }
        }
    }
}
