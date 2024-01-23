using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities;

public class Task
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; } = false;

    // New navigation property and foreign key
    public Guid UserId { get; set; }
    public User User { get; set; }
}
