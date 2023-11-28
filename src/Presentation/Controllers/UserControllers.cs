// MyWebApi.Presentation\Controllers\UserController.cs
using Microsoft.AspNetCore.Mvc;
using DotnetProject.Domain.Entities;
using DotnetProject.Infrastructure.Persistence;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _dbContext.Users.ToList();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _dbContext.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = _dbContext.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        // Update user properties
        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;

        _dbContext.SaveChanges();

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _dbContext.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        return NoContent();
    }
}
