using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Model;

namespace TaskManagerAPI.Controller;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }
    //createUser
    [HttpPost("saveUser")]
    public IActionResult CreateUser(UserDTO userDto)
    {
        // Logic to create a new user in the database
        Console.WriteLine("Creating user: " + userDto.Name);
        try
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (existUser != null)
            {
                return BadRequest("User with this email already exists.");
            }
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Tasks = new List<TaskUser>()
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
        catch (System.Exception)
        {

            throw new Exception("An error occurred while creating the user.");
        }
    }
    //getUserByEmail
    [HttpPost("getUserByEmail")]
    public IActionResult GetUserByEmail(UserDTO userDto)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }
        catch (System.Exception)
        {

            throw new Exception("An error occurred while retrieving the user.");
        }
    }
    //getAllUsers
    [HttpGet("getAllUsers")]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
        catch (System.Exception)
        {

            throw new Exception("An error occurred while retrieving all users.");
        }
        // Logic to retrieve all users from the database
    }
    //updateUser
    [HttpPost("updateUser")]
    public IActionResult UpdateUser(UserDTO userDto)
    {
        // Logic to update user in the database
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }
        catch (System.Exception)
        {

            throw new Exception("An error occurred while updating the user.");
        }
    }
    //deleteUser
    [HttpPost("deleteUser")]
    public IActionResult DeleteUser(UserDTO userDto)
    {
        // Logic to delete user from the database
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok("User deleted successfully.");
        }
        catch (System.Exception)
        {

            throw new Exception("An error occurred while deleting the user.");
        }
    }
}
