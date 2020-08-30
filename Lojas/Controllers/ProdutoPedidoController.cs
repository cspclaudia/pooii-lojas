using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lojas.Data;
using Lojas.Models;

namespace Lojas.Controllers
{
    public class ProdutoPedidoController : Controller
    {
        private readonly LojasContext _context;

        public ProdutoPedidoController(LojasContext context)
        {
            _context = context;
        }

        // GET: ProdutoPedido
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto_Pedido.ToListAsync());
        }

        // GET: ProdutoPedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto_Pedido = await _context.Produto_Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto_Pedido == null)
            {
                return NotFound();
            }

            return View(produto_Pedido);
        }

        // GET: ProdutoPedido/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProdutoPedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantidade")] Produto_Pedido produto_Pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto_Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto_Pedido);
        }

        // GET: ProdutoPedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto_Pedido = await _context.Produto_Pedido.FindAsync(id);
            if (produto_Pedido == null)
            {
                return NotFound();
            }
            return View(produto_Pedido);
        }

        // POST: ProdutoPedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantidade")] Produto_Pedido produto_Pedido)
        {
            if (id != produto_Pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto_Pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Produto_PedidoExists(produto_Pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto_Pedido);
        }

        // GET: ProdutoPedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto_Pedido = await _context.Produto_Pedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto_Pedido == null)
            {
                return NotFound();
            }

            return View(produto_Pedido);
        }

        // POST: ProdutoPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto_Pedido = await _context.Produto_Pedido.FindAsync(id);
            _context.Produto_Pedido.Remove(produto_Pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Produto_PedidoExists(int id)
        {
            return _context.Produto_Pedido.Any(e => e.Id == id);
        }
    }
}
