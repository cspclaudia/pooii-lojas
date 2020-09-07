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

        // public async Task<JsonResult> CartAdd (
        //     int lojaId,
        //     string produto,
        //     int quantidade,
        //     List<Produto_Pedido> carrinho)
        // {
        //     var itens = await _context.Estoque
        //         .Include (m => m.Loja)
        //         .Include (m => m.Produto)
        //         .Where (m => m.LojaId == lojaId).ToListAsync ();

        //     int aux = 0;
        //     //Produto_Pedido teste = (Produto_Pedido)Session["carrinho"];

        //     foreach (Estoque item in itens)
        //     {
        //         if (produto == item.Produto.Nome && quantidade <= item.Quantidade)
        //         {
        //             Produto_Pedido produtoPedido = new Produto_Pedido ()
        //             {
        //                 ProdutoId = item.ProdutoId,
        //                 Quantidade = quantidade,
        //                 Produto = item.Produto
        //             };
        //             carrinho.Add (produtoPedido);
        //             aux++;
        //             break;
        //         }
        //     }
        //     if (aux == 0)
        //     {
        //         ViewBag.Message = "O estoque da loja selecionada não possui essa quantidade do produto.";
        //     }
        //     //var total = carrinho.Select(i => i.Produto).Sum(d => d.Valor);

        //     return new JsonResult (carrinho);
        // }

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