using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Entities;
using TIENDA.Data.SqlServer;
using TIENDA.Models;

namespace TIENDA.Data.Services
{
    public class UsersService : IUserService
    {
        private readonly DBConnection _context;

        public UsersService(DBConnection context)
        {
            _context = context;
        }

        public async Task<TypedResult<List<UserModel>>> ListAsync()
        {
            var result = new TypedResult<List<UserModel>>();

            try
            {
                var model = await _context.Users
                    .Select(x => new UserModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        LastName = x.LastName,
                        Email = x.Email,
                        Created = x.Created,
                        Updated = x.Updated,
                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Code = 1;
                result.Count = model.Count;
                result.Model = model;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al leer la lista de usuarios";
                result.Error = ex;
            }

            return result;
        }
        public async Task<TypedResult<UserModel>> OneAsync(int userId)
        {
            var result = new TypedResult<UserModel>();

            try
            {
                var model = await _context.Users
                    .Where(x => x.Id == userId)
                    .Select(x => new UserModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        LastName = x.LastName,
                        Email = x.Email,
                        Created = x.Created,
                        Updated = x.Updated,
                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Code = 1;
                result.Count = model.Count;
                result.Model = model.FirstOrDefault();

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al leer el usuario especificado";
                result.Error = ex;
            }

            return result;
        }

        public async Task<MsgResult> InsertAsyc(UserRegisterModel model)
        {
            var result = new MsgResult();

            var entity = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (entity != null)
            {
                result.IsSuccess = false;
                result.Message = $"Ya se encuentra registrado un usuario con el email {model.Email}";
                return result;
            }

            var passwordHashsed = Encrypt.MD5Encrypt(model.Password);

            entity = new User
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = passwordHashsed,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Usuario registrado correctamente";
                result.Code = entity.Id;
            }
            else
            {
                result.Message = "Error al registrar el usuario";
            }
            return result;
        }
        public async Task<MsgResult> UpdateAsync(UserModel model)
        {
            var result = new MsgResult();

            var entity = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = $"Usuario no encontrado";
                return result;
            }

            entity.Name = model.Name;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Updated = DateTime.Now;

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Usuario modificado correctamente";
            }
            else
            {
                result.Message = "Error al modificar los datos del usuario";
            }
            return result;
        }
        public async Task<MsgResult> DeleteAsync(int userId)
        {
            var result = new MsgResult();

            var entity = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = $"Usuario no encontrado";
                return result;
            }


            _context.Users.Remove(entity);

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Usuario eliminado correctamente";
            }
            else
            {
                result.Message = "Error al eliminar el usuario";
            }
            return result;
        }

        public async Task<TypedResult<UserModel>> Login(UserLoginModel model)
        {
            var result = new TypedResult<UserModel>();


            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Email == model.Email);

            if (user==null)
            {
                result.IsSuccess = false;
                result.Message = "Usuario no válido";
                return result;
            }


            var hashedPassword = model.Password.MD5Encrypt();

            if (user.Password != hashedPassword)
            {
                result.IsSuccess = false;
                result.Message = "Contraseña no válida";
                return result;
            }


            try
            {
                var query     = await _context.Users
                    .Where(x=>x.Email == model.Email)
                    .Select(x => new UserModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        LastName = x.LastName,
                        Email = x.Email,
                        Created = x.Created,
                        Updated = x.Updated,
                    }).ToListAsync();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Code = 1;
                result.Count = query.Count;
                result.Model = query.FirstOrDefault();

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al leer el usuario especificado";
                result.Error = ex;
            }

            return result;
        }
        public Task<MsgResult> RecoverPassword(RecoverPasswordModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<MsgResult> ChangePassword(ChangePasswordModel model)
        {
            var result = new MsgResult();

            var entity = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = $"Usuario no encontrado";
                return result;
            }

            entity.Password = model.NewPassword.MD5Encrypt();
            entity.Updated = DateTime.Now;

            result = await _context.SaveAsync();

            if (result.IsSuccess)
            {
                result.Message = "Contraseña cambiada correctamente";
            }
            else
            {
                result.Message = "Error al cambiar la contraseña del usuario";
            }
            return result;
        }
    }
}
