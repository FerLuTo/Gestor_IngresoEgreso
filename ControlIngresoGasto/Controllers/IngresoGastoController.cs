using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlIngresoGasto.Data;
using ControlIngresoGasto.Models;

namespace ControlIngresoGasto.Controllers
{
    public class IngresoGastoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngresoGastoController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Get: IngresoGasto
        public async Task<IActionResult> Index(int? mes,int? anio)
        {
            if(mes==null)
            {
                mes = DateTime.Now.Month;
            }
            if (anio == null)
            {
                anio = DateTime.Now.Year;
            }

            ViewData["mes"] = mes;
            ViewData["anio"] = anio;

            var applicationDbContext = _context.IngresoGasto.Include(i => i.Categoria)
                                        .Where(i=>i.Fecha.Month == mes && i.Fecha.Year == anio);
            return View(await applicationDbContext.ToListAsync());
        }

        //Get: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGasto
                .Include(i=>i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }

            return View(ingresoGasto);
        }

        //get: Categorias/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(c=>c.Estado==true), "Id", "NombreCategoria");
            return View();
        }

        //Post: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaId,Fecha,Valor")] IngresoGasto ingresoGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingresoGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }


        //Get: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGasto
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        
        }


        //Post: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriaId,Fecha,Valor")] IngresoGasto ingresoGasto)
        {
            if (id != ingresoGasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingresoGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoGastoExist(ingresoGasto.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", ingresoGasto.CategoriaId);
            return View(ingresoGasto);

        }
        // GET: IngresoGasto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGasto
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }

            return View(ingresoGasto);
        }

        // POST: IngresoGasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingresoGasto = await _context.IngresoGasto.FindAsync(id);
            _context.IngresoGasto.Remove(ingresoGasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool IngresoGastoExist(int id)
        {
            throw new NotImplementedException();
        }
    }
}
