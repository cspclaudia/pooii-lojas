using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lojas.Data;
using Lojas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lojas.Controllers
{
    public class PedidoController : Controller
    {
        private readonly LojasContext _context;

        public PedidoController (LojasContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index ()
        {
            var lojasContext = _context.Pedido.Include (p => p.Loja);
            return View (await lojasContext.ToListAsync ());
        }

        // GET: ProdutoPedido/Details/5
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var produto_Pedido = await _context.Produto_Pedido
                .Include (p => p.Pedido)
                .Include (p => p.Produto)
                .Where (m => m.PedidoId == id).ToListAsync ();
            if (produto_Pedido == null)
            {
                return NotFound ();
            }

            return View (produto_Pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create ()
        {
            ViewData["LojaId"] = new SelectList (_context.Loja, "Id", "Nome");
            ViewData["ProdutoId"] = new SelectList (_context.Produto, "Id", "Nome");
            return View ();
        }

        public async Task<JsonResult> ListItens (int lojaId)
        {
            var itens = await _context.Estoque
                .Include (m => m.Loja)
                .Include (m => m.Produto)
                .Where (m => m.LojaId == lojaId).ToListAsync ();
            return new JsonResult (itens);
        }

        public async Task<JsonResult> Save ([Bind ("Id,Cliente,LojaId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add (pedido);
                await _context.SaveChangesAsync ();
            }
            return new JsonResult (pedido);
        }

        public async Task<JsonResult> GetTotal (int PedidoId)
        {
            var valor = await _context.Pedido
                .Where (m => m.Id == PedidoId)
                .Select (m => m.Valor).ToListAsync ();
            return new JsonResult (valor);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var pedido = await _context.Pedido
                .Include (p => p.Loja)
                .FirstOrDefaultAsync (m => m.Id == id);
            if (pedido == null)
            {
                return NotFound ();
            }

            return View (pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var pedido = await _context.Pedido.FindAsync (id);
            _context.Pedido.Remove (pedido);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool PedidoExists (int id)
        {
            return _context.Pedido.Any (e => e.Id == id);
        }
    }
}