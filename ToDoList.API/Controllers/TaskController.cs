using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.Tasks;
using ToDoList.Contracts.Tasks;
using ToDoList.Application.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        /// <summary>
        /// Add a new todo.
        /// </summary>
        /// <param name="taskRequest">new Task data</param>
        /// <returns>New task created.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> CreateTask([FromBody] TaskRequest taskRequest)
        {
            string? userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var task = await _taskService.CreateTaskAsync(taskRequest, userId);
            return Ok(task);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetTasks()
        {
            string? userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var tasks = await _taskService.GetAllTaskByIdAsync(userId);

            return Ok(tasks);
        }

        [HttpPut("add")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskRequest taskRequest)
        {
            string? userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }
            var task = await _taskService.CreateTaskAsync(taskRequest, userId);
            return Ok(task);
        }

        [HttpPut("update/{taskId}")]
        public async Task<IActionResult> UpdateTask([FromRoute] int taskId, [FromBody] TaskRequest updatedTask)
        {
            string? userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }

            var result = await _taskService.UpdateTaskAsync(taskId, updatedTask, userId);

            if (result == null)
            {
                return NotFound(); // The TAsk wasn't found.
            }

            return Ok(result);
        }

        [HttpDelete("delete/{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int taskId)
        {
            string? userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null)
            {
                return BadRequest();
            }

            var result = await _taskService.DeleteTaskAsync(taskId, userId);

            if (!result)
            {
                return NotFound(); // La tarea no fue encontrada o no pertenece al usuario
            }

            return NoContent(); // Éxito en la eliminación
        }
    }
}
