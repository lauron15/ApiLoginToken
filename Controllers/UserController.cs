using ApiLoginToken.Data;
using ApiLoginToken.Dto;
using ApiLoginToken.Interfaces;
using ApiLoginToken.Mappers;
using ApiLoginToken.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoginToken.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase

    {
        private readonly ApplicationDBContext _context;
        private readonly IUserRepository _userRepo;
        private readonly TokenService _tokenService;

        public UserController(ApplicationDBContext context, IUserRepository userRepository, TokenService tokenService)
        {
            _userRepo = userRepository;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var userModel = users.Select(s => s.ToUsersDto());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUsersDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCadastroDto userDto)
        {
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(userDto.Senha); //metodo para cryptografar
            var userModel = userDto.ToUserFromCreateDTO();
            userModel.Senha = senhaCriptografada; // segunda parte da crypografia

            await _userRepo.CreateAsync(userModel);
            var token = _tokenService.Generate(userModel);
            //  return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUsersDto()); Metodo sem o retorno do token.
            //Metodo com o retorno do token. 
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, new {token= token, User = userModel.ToUsersDto() });
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Upadate([FromRoute] int id, [FromBody] UpdateUsersDto upadateDto)
        {
            var userModel = await _userRepo.UpdateAsync(id, upadateDto);
            if (userModel == null)
            {
                return NotFound();
            }

            userModel.Nome = upadateDto.Nome;
            userModel.Email = upadateDto.Email;
            userModel.Senha = upadateDto.Senha;
            await _context.SaveChangesAsync();
            return Ok(userModel.ToUsersDto());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _userRepo.DeleteAsync(id);
            if(userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return NoContent();
        
        }
    }
}
