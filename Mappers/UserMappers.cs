using ApiLoginToken.Dto;
using ApiLoginToken.Models;

namespace ApiLoginToken.Mappers
{
    public static class UserMappers
    {
        public static UsuarioDto ToUsersDto(this User userModel)
        {
            return new UsuarioDto
            {
                Id = userModel.Id,
                Nome = userModel.Nome,
                Email = userModel.Email,
            };
        }

        public static User ToUserFromCreateDTO(this UsuarioCadastroDto userDto)
        {
            return new User
            {
                Nome = userDto.Nome,
                Email = userDto.Email,
                Senha = userDto.Senha, // Certifique-se de hash posteriormente
            };
        }
        public static void UpdateUserFromDto(this User userModel, UpdateUsersDto userDto)
        {
            if (!string.IsNullOrEmpty(userDto.Nome))
                userModel.Nome = userDto.Nome;

            if (!string.IsNullOrEmpty(userDto.Email))
                userModel.Email = userDto.Email;

            if (!string.IsNullOrEmpty(userDto.Senha))
                userModel.Senha = userDto.Senha; // Hash se necessário
        }

    }

}
