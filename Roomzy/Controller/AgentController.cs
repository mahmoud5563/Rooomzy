using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roomzy.Models;
using System;
using System.Threading.Tasks;

namespace Roomzy.Controllers
{
    public class AgentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AgentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Agents/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Agents/Add
        [HttpPost]
        public async Task<IActionResult> Add(AddAgentsVeiwModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var agent = new Agents
                {
                    Id = Guid.NewGuid(), // Generate a new GUID for the agent
                    First_Name = viewModel.First_Name,
                    Last_Name = viewModel.Last_Name,
                    User_Name = viewModel.User_Name,
                    User_Password = viewModel.User_Password,
                    User_Email = viewModel.User_Email,
                    User_Phone = viewModel.User_Phone,
                    User_Address = viewModel.User_Address,
                    Remark = viewModel.Remark,
                    Deleted = false // Set to false as the agent is not deleted initially
                };

                await dbContext.Agents.AddAsync(agent);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        // GET: Agents/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Retrieve only non-deleted agents
            var agents = await dbContext.Agents.Where(a => !a.Deleted).ToListAsync();
            return View(agents);
        }

        // GET: Agents/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var agent = await dbContext.Agents.FindAsync(id);

            if (agent == null || agent.Deleted)
            {
                return NotFound();
            }

            var viewModel = new AddAgentsVeiwModel
            {
                ID = agent.Id,
                First_Name = agent.First_Name,
                Last_Name = agent.Last_Name,
                User_Name = agent.User_Name,
                User_Password = agent.User_Password,
                User_Email = agent.User_Email,
                User_Phone = agent.User_Phone,
                User_Address = agent.User_Address,
                Remark = agent.Remark
            };

            return View(viewModel);
        }

        // POST: Agents/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(AddAgentsVeiwModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var agent = await dbContext.Agents.FindAsync(viewModel.ID);

                if (agent == null || agent.Deleted)
                {
                    return NotFound();
                }

                agent.First_Name = viewModel.First_Name;
                agent.Last_Name = viewModel.Last_Name;
                agent.User_Name = viewModel.User_Name;
                agent.User_Password = viewModel.User_Password;
                agent.User_Email = viewModel.User_Email;
                agent.User_Phone = viewModel.User_Phone;
                agent.User_Address = viewModel.User_Address;
                agent.Remark = viewModel.Remark;

                dbContext.Agents.Update(agent);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        // GET: Agents/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var agent = await dbContext.Agents.FindAsync(id);

            if (agent == null || agent.Deleted)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agent = await dbContext.Agents.FindAsync(id);

            if (agent == null || agent.Deleted)
            {
                return NotFound();
            }

            // Soft delete: Mark as deleted
            agent.Deleted = true;
            dbContext.Agents.Update(agent);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }
    }
}
