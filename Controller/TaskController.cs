using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
namespace TaskManagerAPI.Controller;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Model;

[ApiController]
[Route("api/v1/tasks")]
    public class TaskController : ControllerBase
    {
          private readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }
    
    //saveTask
    [HttpPost("saveTask")]
    public IActionResult CreateTask(TaskDTO taskDto)
    {
        Console.WriteLine("Creating task: " + taskDto.Title);
        Console.WriteLine("User email: " + taskDto.Email);
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == taskDto.Email);
            Console.WriteLine("User found: " + (user != null ? user.Name : "No user found"));
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var task = new TaskUser
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Email = taskDto.Email,
                User = user
            };
            _context.Tasks.Add(task);
            _context.SaveChanges();
            Console.WriteLine("Task created successfully: " + task.Title);
            
            var response = new TaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Email = task.Email,
                User = new UserBasicDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }
            };
            return Ok(response);
        }
        catch (System.Exception)        {
            throw new Exception("An error occurred while creating the task.");
        }
    }
    //getAllTasks
    [HttpGet("getAllTasks")]
    public IActionResult GetAllTasks()
    {
        try
        {
            var tasks = _context.Tasks.Include(t => t.User).ToList();
            var response = tasks.Select(t => new TaskResponseDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Email = t.Email,
                User = new UserBasicDTO
                {
                    Id = t.User.Id,
                    Name = t.User.Name,
                    Email = t.User.Email
                }
            }).ToList();
            return Ok(response);
        }
        catch (System.Exception)
        {
            throw new Exception("An error occurred while retrieving tasks.");
        }
    }

        
    }
