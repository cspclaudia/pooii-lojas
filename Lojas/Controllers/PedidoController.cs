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

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details (int? id)
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

        // GET: Pedido/Create
        public IActionResult Create ()
        {
            ViewData["LojaId"] = new SelectList (_context.Loja, "Id", "Nome");
            ViewData["ProdutoId"] = new SelectList (_context.Produto, "Id", "Nome");
            return View ();
        }

        // POST: Pedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,Cliente,Valor,LojaId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.Data = DateTime.Now;
                _context.Add (pedido);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            ViewData["LojaId"] = new SelectList (_context.Loja, "Id", "Nome", pedido.LojaId);
            return View (pedido);
        }

        public async Task<JsonResult> ListItens (int lojaId)
        {
            var itens = await _context.Estoque
                .Include (m => m.Loja)
                .Include (m => m.Produto)
                .Where (m => m.LojaId == lojaId).ToListAsync ();
            return new JsonResult (itens);
        }

        public async Task<JsonResult> GetTotal (int PedidoId)
        {
            var valor = await _context.Pedido
                .Where (m => m.Id == PedidoId)
                .Select (m => m.Valor).ToListAsync ();
            return new JsonResult (valor);
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

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var pedido = await _context.Pedido.FindAsync (id);
            if (pedido == null)
            {
                return NotFound ();
            }
            ViewData["LojaId"] = new SelectList (_context.Loja, "Id", "Nome", pedido.LojaId);
            return View (pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Cliente,Data,Valor,LojaId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound ();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (pedido);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists (pedido.Id))
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            ViewData["LojaId"] = new SelectList (_context.Loja, "Id", "Nome", pedido.LojaId);
            return View (pedido);
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