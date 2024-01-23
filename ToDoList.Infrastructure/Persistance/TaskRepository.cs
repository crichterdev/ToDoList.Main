using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Persistance;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public TaskRepository(ApplicationDbContext applicationDbContext)
    {
        _appDbContext = applicationDbContext;

    }
    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        
        var taskToDelete = await _appDbContext.Tasks.FindAsync(id);

        if (taskToDelete != null)
        {
            _appDbContext.Tasks.Remove(taskToDelete);
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entities.Task>> GetAllAsync()
    {
        return await _appDbContext.Tasks.ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Task>> GetAllAsync(Guid id)
    {
        return await _appDbContext.Tasks.Where(t => t.UserId.Equals(id)).ToListAsync();
    }

    public async Task<Domain.Entities.Task> GetByIdAsync(int taskId, Guid userId)
    {
        
        return await _appDbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId)
            ?? throw new InvalidOperationException($"Task with ID {taskId} not found for user with ID {userId}");
    }


    public async System.Threading.Tasks.Task Insert(Domain.Entities.Task task)
    {
        await _appDbContext.Tasks.AddAsync(task);
        _appDbContext.SaveChanges();
       
    }

    public async System.Threading.Tasks.Task Update(Domain.Entities.Task task)
    {
       
        var existingTask = await _appDbContext.Tasks.FindAsync(task.Id);

        if (existingTask != null)
        {
            existingTask.Title = task.Title;
            existingTask.IsDone = task.IsDone;

            await _appDbContext.SaveChangesAsync();
           
        }
    }
}
