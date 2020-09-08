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
        public IActionResult Create()
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

        public List<Produto_Pedido> carrinho = new List<Produto_Pedido> ();
        public async Task<JsonResult> CartAdd (
            int lojaId,
            string produto,
            int quantidade)
        {
            var itens = await _context.Estoque
                .Include (m => m.Loja)
                .Include (m => m.Produto)
                .Where (m => m.LojaId == lojaId).ToListAsync ();

            Estoque itemEstoque = new Estoque();
            itemEstoque = itens.Where(m => m.Produto.Nome == produto).FirstOrDefault();

            Produto_Pedido produto_Pedido = new Produto_Pedido ()
                    {
                        Quantidade = quantidade,
                        ProdutoId = itemEstoque.ProdutoId
                    };

            if(produto_Pedido != null && quantidade <= itemEstoque.Quantidade)
            {
                //INSERT INTO Produto_Pedido (Quantidade,ProdutoId)
                if (ModelState.IsValid)
                {
                    var add = _context.Add(produto_Pedido);
                    var salvar = await _context.SaveChangesAsync();
                }
            }
            
            

            //var item = itens.Where(m => m.Produto.Nome == produto && m.Quantidade == quantidade).FirstOrDefault();
            // int aux = 0;
            //Produto_Pedido teste = (Produto_Pedido)Session["carrinho"];
            // foreach (Estoque item in itens)
            // {
            //     if (produto == item.Produto.Nome && quantidade <= item.Quantidade)
            //     {
            //         Produto_Pedido produto_Pedido = new Produto_Pedido ()
            //         {
            //             Produto = item.Produto,
            //             ProdutoId = item.ProdutoId,
            //             Quantidade = quantidade
                        
            //         };
            //         carrinho.Add (produto_Pedido);
            //         aux++;
            //         break;
            //     }
            // }
            

            // if (aux == 0)
            // {
            //     ViewBag.Message = "O estoque da loja selecionada não possui essa quantidade do produto.";
            // }
            //var total = carrinho.Select(i => i.Produto).Sum(d => d.Valor);

            return new JsonResult (produto_Pedido);
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
