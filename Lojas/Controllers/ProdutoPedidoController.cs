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
    public class ProdutoPedidoController : Controller
    {
        private readonly LojasContext _context;

        public ProdutoPedidoController (LojasContext context)
        {
            _context = context;
        }

        // GET: ProdutoPedido
        public async Task<IActionResult> Index ()
        {
            var lojasContext = _context.Produto_Pedido.Include (p => p.Pedido).Include (p => p.Produto);
            return View (await lojasContext.ToListAsync ());
        }

        public async Task<JsonResult> CartAdd (
            [Bind ("Id,Quantidade,ProdutoId")] Produto_Pedido produto_Pedido, 
            [Bind ("Id,Cliente,Valor,LojaId")] Pedido pedido)
        {
            var itens = await _context.Estoque
                .Include (m => m.Produto)
                .Where (m => m.LojaId == pedido.LojaId).ToListAsync ();

            Estoque itemEstoque = new Estoque ();
            itemEstoque = itens.Where (m => m.ProdutoId == produto_Pedido.ProdutoId).FirstOrDefault ();

            produto_Pedido.PedidoId = pedido.Id;

            if (produto_Pedido != null && produto_Pedido.Quantidade <= itemEstoque.Quantidade)
            {
                if (ModelState.IsValid)
                {
                    _context.Add (produto_Pedido);
                    await _context.SaveChangesAsync ();
                }

                itemEstoque.Quantidade -= produto_Pedido.Quantidade;
                _context.Update (itemEstoque);
                await _context.SaveChangesAsync ();
            }

            var carrinho = await _context.Produto_Pedido
                .Include (m => m.Produto)
                .Where (m => m.PedidoId == produto_Pedido.PedidoId).ToListAsync ();

            foreach (var item in carrinho)
                pedido.Valor += item.Produto.Valor * item.Quantidade;

            pedido.Data = DateTime.Now;
            _context.Update (pedido);
            await _context.SaveChangesAsync ();

            return new JsonResult (carrinho);
        }

        private bool Produto_PedidoExists (int id)
        {
            return _context.Produto_Pedido.Any (e => e.Id == id);
        }
    }
}