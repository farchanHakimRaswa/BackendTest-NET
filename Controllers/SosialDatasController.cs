using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bismillah1.Models;

namespace Bismillah1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SosialDatasController : ControllerBase
    {
        private readonly TodoContext _context;

        public SosialDatasController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/SosialDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SosialData>>> GetSosialData()
        {
            return await _context.SosialData.ToListAsync();
        }

        // GET: api/SosialDatas/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<SosialData>> GetSosialData(long id)
        {
            var sosialData = await _context.SosialData.FindAsync(id);

            if (sosialData == null)
            {
                return NotFound();
            }

            return sosialData;
        }
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<SosialData>>> GetDataByName(String name)
        {
            return await _context.SosialData.Where(p => p.Name == name).ToListAsync() ;
        }

        // PUT: api/SosialDatas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<SosialData>> PutSosialData(long id, SosialData sosialData)
        {
            var checkData = await _context.SosialData.FindAsync(id);
            if (checkData == null)
            {
                return NotFound();
            }

            //sosialData.Name = "Bill";
            //await _context.SaveChangesAsync();

            // _context.Entry(await _context.SosialData.FirstOrDefaultAsync(x => x.Id == sosialData.Id)).CurrentValues.SetValues(sosialData);
            //await _context.SaveChangesAsync();
            _context.Entry(sosialData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SosialDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return sosialData;

        }

        // POST: api/SosialDatas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SosialData>> PostSosialData(SosialData sosialData)
        {
            _context.SosialData.Add(sosialData);
            await _context.SaveChangesAsync();

          //  return CreatedAtAction("GetSosialData", new { id = sosialData.Id }, sosialData);
            return CreatedAtAction(nameof(GetSosialData), new { id = sosialData.Id }, sosialData);
        }

        // DELETE: api/SosialDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SosialData>> DeleteSosialData(long id)
        {
            var sosialData = await _context.SosialData.FindAsync(id);
            if (sosialData == null)
            {
                return NotFound();
            }

            _context.SosialData.Remove(sosialData);
            await _context.SaveChangesAsync();

            return sosialData;
        }

        private bool SosialDataExists(long id)
        {
            return _context.SosialData.Any(e => e.Id == id);
        }
    }
}
