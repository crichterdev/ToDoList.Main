using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Interfaces.Common;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

}
