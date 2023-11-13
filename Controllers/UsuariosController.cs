using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuE_Teste.Data;
using AuE_Teste.Models;

namespace AuE_Teste.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Context _context;

        public UsuariosController(Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Usuario != null ? 
        //                  View(await _context.Usuario.ToListAsync()) :
        //                  Problem("Entity set 'Context.Usuario'  is null.");
        //}



        public async Task<IActionResult> Index()
        {
            // Verifica se há usuários no contexto
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'Context.Usuario' is null.");
            }

            // Obtém a lista de usuários
            var usuarios = await _context.Usuario.ToListAsync();

            // Passa a lista de usuários para a view
            return View(usuarios);
        }

      


        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sexo,Data, Cidade")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sexo,Data,Cidade")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'Context.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuario?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public IActionResult RelatorioContatosPorCidade()
        {
            var relatorio = _context.Usuario
                .GroupBy(u => new { u.Cidade, u.Data.Month })
                .Select(g => new
                {
                    Cidade = g.Key.Cidade,
                    Mes = g.Key.Month,
                    TotalContatos = g.Count()
                })
                .OrderBy(r => r.Cidade)
                .ThenBy(r => r.Mes)
                .ToList();

            ViewBag.Relatorio = relatorio;

            return View("RelatorioContatosPorCidade", relatorio);
        }


      


        public IActionResult Estatisticas()
        {
            var estatisticas = _context.Usuario
                .GroupBy(u => u.Cidade)
                .Select(g => new EstatisticasViewModel
                {
                    Cidade = g.Key,
                    TotalCadastros = g.Count(),
                    QuantidadePorSexo = g.GroupBy(u => u.Sexo)
                                         .Select(s => new SexoQuantidadeViewModel
                                         {
                                             Sexo = s.Key,
                                             Quantidade = s.Count()
                                         })
                })
                .ToList();

            return View(estatisticas);
        }

        public IActionResult MostrarLista()
        {
            ViewBag.MostrarLista = true;

            // Obtém a lista de usuários
            var usuarios = _context.Usuario.ToList();

            // Passa a lista de usuários para a view
            return View("Index", usuarios);
        }


    }
}
