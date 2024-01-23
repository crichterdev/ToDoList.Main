using ToDoList.Contracts.Tasks;

namespace ToDoList.Application.Interfaces;

public interface ITaskService
{
    Task<Domain.Entities.Task> CreateTaskAsync(TaskRequest taskRequest, string userId);
    Task<IEnumerable<Domain.Entities.Task>> GetAllTaskByIdAsync(string userId);
    Task<Domain.Entities.Task?> UpdateTaskAsync(int taskId, TaskRequest updatedTask, string userId);
    Task<bool> DeleteTaskAsync(int taskId, string userId);
}
