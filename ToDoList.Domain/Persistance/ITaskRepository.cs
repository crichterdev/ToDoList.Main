namespace ToDoList.Domain.Entities;

public interface ITaskRepository
{
    Task<IEnumerable<Task>> GetAllAsync(Guid id);
    Task<Task> GetByIdAsync(int taskId, Guid userId);
    System.Threading.Tasks.Task DeleteAsync(int id);
    System.Threading.Tasks.Task Insert(Task task);
    System.Threading.Tasks.Task Update(Task task);

}