using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.DTOs;

    public class TaskDTO
    {
        public int Id { get; set; }
        public required string  Title { get; set; }
        public required string Description { get; set; }
        public required string Email { get; set; }
        public required int UserId { get; set; }

        
    }
