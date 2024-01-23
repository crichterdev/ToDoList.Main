using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces;
using ToDoList.Contracts.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Tasks;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<Domain.Entities.Task> CreateTaskAsync(TaskRequest taskRequest, string userId)
    {
        var userGuid = new Guid(userId);
        var task = taskRequest.Adapt<Domain.Entities.Task>();
        task.UserId= userGuid;
        //- Logic to avoid insert duplicate records.
        List<Domain.Entities.Task> tasks = (await _taskRepository.GetAllAsync(userGuid)).ToList();
        if (!tasks.Any(t => t.Title.Equals(task.Title)
        && t.IsDone.Equals(task.IsDone)))
        {
            await _taskRepository.Insert(task);
            return task;
        }
        else
        {
            return null;
        }

    }

    public async Task<IEnumerable<Domain.Entities.Task>> GetAllTaskByIdAsync(string userId)
    {
        var userGuid = new Guid(userId);
        return await _taskRepository.GetAllAsync(userGuid); 
    }

    public async Task<Domain.Entities.Task?> UpdateTaskAsync(int taskId, TaskRequest updatedTask, string userId)
    {
        var userGuid = new Guid(userId);
        var existingTask = await _taskRepository.GetByIdAsync(taskId, userGuid);

        if (existingTask == null)
        {
            return null; // Task not found
        }

        // Update properties of the existing task
        existingTask.Title = updatedTask.Title;        
        existingTask.IsDone = updatedTask.IsDone;

        await _taskRepository.Update(existingTask);

        return existingTask;
    }

    public async Task<bool> DeleteTaskAsync(int taskId, string userId)
    {
        var userGuid = new Guid(userId);
        var existingTask = await _taskRepository.GetByIdAsync(taskId, userGuid);

        if (existingTask == null)
        {
            return false; // Task not found
        }

        await _taskRepository.DeleteAsync(existingTask.Id);
        return true; // Deletion success
    }
}
