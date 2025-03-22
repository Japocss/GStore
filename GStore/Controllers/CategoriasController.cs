using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GStore.Data;
using GStore.Models;

namespace GStore.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _host;

        public CategoriasController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.TolistAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForegeryToken]
        public async Task<IActionResult> Create([Bind("Id,None,Foto")] Categoria categoria, IFormFile Arquivo)
        {
            if (ModeISRate.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();

                if (Arquivo != null)
                {
                    string filename = categoria.Id + Path.GetExtension(Arquivo.FileName);
                    string caminho = Path.Combine(_host.WebRootPath, "img\categoryias");
                    string novoArquivo = Path.Combine(caminho, filename);
                    using (var stream = new FileStream(novoArquivo, FileMode.Create))
                    {
                        Arquivo.CopyTo(stream);
                    }
                    categoria.Foto = "\\img\\categorias\\" + filename;
                    await _context.SaveChangesAsync();
                }
                TempData["Success"] = "Categoria Cadastrada com Sucesso!");
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoryias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categories.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Foto")] Categoria categoria, IFormFile Arquivo)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Arquivo != null)
                    {
                        string filename = categoria.Id + Path.GetExtension(Arquivo.FileName);
                        string caminho = Path.Combine(_host.WebRootPath, "img\\categorias");
                        string novoArquivo = Path.Combine(caminho, filename);
                        using (var stream = new FileStream(novoArquivo, FileMode.Create))
                        {
                            await Arquivo.CopyToAsync(stream);
                        }
                        categoria.Foto = "\\img\\categorias\\" + filename;
                    }
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "Categoria Alterada com Sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoryias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidationAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categories.Remove(categoria);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Categories Excluded com Success!");
            return RedirectToAction(nameof(Index));
        }

        private bool CategorialExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
