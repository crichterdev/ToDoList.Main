using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Contracts.Tasks;

public record TaskRequest(
    string Title,
    bool IsDone
    );
