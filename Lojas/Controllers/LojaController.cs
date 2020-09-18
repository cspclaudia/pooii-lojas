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
    public class LojaController : Controller
    {
        private readonly LojasContext _context;

        public LojaController (LojasContext context)
        {
            _context = context;
        }

        // GET: Loja
        public async Task<IActionResult> Index ()
        {
            return View (await _context.Loja.ToListAsync ());
        }

        // GET: ProdutoPedido/Details/5
        public async Task<IActionResult> Estoque (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var estoque = await _context.Estoque
                .Include (p => p.Loja)
                .Include (p => p.Produto)
                .Where (m => m.LojaId == id).ToListAsync ();
            if (estoque == null)
            {
                return NotFound ();
            }

            return View (estoque);
        }

        public RedirectToActionResult EditEstoque (int? id)
        {
            return RedirectToAction ("Edit", "Estoque");
        }

        // GET: Loja/Create
        public IActionResult Create ()
        {
            return View ();
        }

        // POST: Loja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Id,Nome,Local")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                _context.Add (loja);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (loja);
        }

        // GET: Loja/Edit/5
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var loja = await _context.Loja.FindAsync (id);
            if (loja == null)
            {
                return NotFound ();
            }
            return View (loja);
        }

        // POST: Loja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, [Bind ("Id,Nome,Local")] Loja loja)
        {
            if (id != loja.Id)
            {
                return NotFound ();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update (loja);
                    await _context.SaveChangesAsync ();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LojaExists (loja.Id))
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
            return View (loja);
        }

        // GET: Loja/Delete/5
        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return NotFound ();
            }

            var loja = await _context.Loja
                .FirstOrDefaultAsync (m => m.Id == id);
            if (loja == null)
            {
                return NotFound ();
            }

            return View (loja);
        }

        // POST: Loja/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var loja = await _context.Loja.FindAsync (id);
            _context.Loja.Remove (loja);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool LojaExists (int id)
        {
            return _context.Loja.Any (e => e.Id == id);
        }
    }
}