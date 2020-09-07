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
            var lojasContext = _context.Produto_Pedido.Include(p => p.Pedido).Include(p => p.Produto);
            return View(await lojasContext.ToListAsync());
        }

        // GET: ProdutoPedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto_Pedido = await _context.Produto_Pedido
                .Include(p => p.Pedido)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto_Pedido == null)
            {
                return NotFound();
            }

            return View(produto_Pedido);
        }

        // GET: ProdutoPedido/Create
        public IActionResult LittleCarAdd()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome");
            return View();
        }

        // POST: ProdutoPedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantidade,ProdutoId,PedidoId")] Produto_Pedido produto_Pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto_Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Cliente", produto_Pedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", produto_Pedido.ProdutoId);
            return View(produto_Pedido);
        }

        // List<Produto_Pedido> carrinho = new List<Produto_Pedido>();
        // public async Task<JsonResult> LittleCarAdd(int LojaId, [Bind("Id,Quantidade,ProdutoId")] Produto_Pedido item)
        // {
        //     Console.WriteLine("Chamou!");

        //     var estoque = await _context.Estoque
        //         .Include(m => m.Loja)
        //         .Include(m => m.Produto)
        //         .Where(m => m.LojaId == LojaId).ToListAsync();

        //     int aux = 0; Produto produtoAux = new Produto();
        //     foreach (Estoque produto in estoque)
        //     {
        //         if (item.PedidoId == produto.ProdutoId && item.Quantidade <= produto.Quantidade)
        //         {
        //             carrinho.Add(item);
        //             aux++;
        //             break;
        //         }
        //     }
        //     if (aux == 0)
        //     {
        //             Console.WriteLine("O estoque dessa loja não possui o produto ou a quantidade é insuficiente.");
        //     }
        //     var total = carrinho.Select(i => i.Produto).Sum(d => d.Valor);

        //     Console.Write("Carrinho: ", carrinho);
        //     Console.Write("Valor Total: ", carrinho);

        //     return new JsonResult(carrinho, total);
        // }

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
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Cliente", produto_Pedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", produto_Pedido.ProdutoId);
            return View(produto_Pedido);
        }

        // POST: ProdutoPedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantidade,ProdutoId,PedidoId")] Produto_Pedido produto_Pedido)
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
            ViewData["PedidoId"] = new SelectList(_context.Pedido, "Id", "Cliente", produto_Pedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", produto_Pedido.ProdutoId);
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
                .Include(p => p.Pedido)
                .Include(p => p.Produto)
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
