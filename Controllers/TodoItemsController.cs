using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTestProject.Models;

namespace NetTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems : Get all Data in database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5 : Get spesific data by id unique (primary key)
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // GET : api/TodoItems/byDate/2020-08-20/2020-08-21 : Get all data with spesific range date (start date and end date)
        [HttpGet("byDate/{startDate}/{endDate}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemByDate(DateTime startDate, DateTime endDate)
        {
           return await _context.TodoItems.Where(p => p.FinishDate >= startDate && p.FinishDate <= endDate).ToListAsync();
        }

        // GET : api/TodoItems/byStatus/false : Get all data with spesific status (true or false)
        [HttpGet("byStatus/{status}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemByStatus(bool status)
        {
            return await _context.TodoItems.Where(p => p.isComplate == status).ToListAsync();
        }


        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(todoItem);
        }

        // PATCH: api/TodoItems/setComplate/1/true : is used to set status complate (true or false) of spesific todo id
        [HttpPatch("setComplate/{id}/{percent}")]
        public async Task<ActionResult<TodoItem>> SetisComplate(bool percent, long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.isComplate = percent;
            await _context.SaveChangesAsync();
            return todoItem;
        }


        // PATCH: api/TodoItems/UpdatePercent/1/30 : is used to set percent value of spesific todo id
        [HttpPatch("UpdatePercent/{id}/{percent}")]
        public async Task<ActionResult<TodoItem>> SetPercentComplate(long percent, long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.percentComplate = percent;
            await _context.SaveChangesAsync();
            return todoItem;
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }



        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
