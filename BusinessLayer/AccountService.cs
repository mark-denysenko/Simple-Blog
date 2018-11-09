using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core;
using BusinessLayer.BusinessModelsDTO;
using BusinessLayer.Interfaces;
using Interfaces;
using Services.Interfaces;

namespace BusinessLayer
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHasherPassword _hasherPassword;

        public AccountService(IUnitOfWork uow, IHasherPassword hasherPass)
        {
            _uow = uow;
            _hasherPassword = hasherPass;
        }

        public UserProfile GetUserProfile(string nickname)
        {
            if(nickname == null)
                throw new ArgumentException("Empty nickname string. Cannot return profile");

            User user = _uow.Users.GetAll().FirstOrDefault(u => u.Nickname == nickname);

            if (user == null)
                return null;

            var userProfile = Mapper.Map<UserProfile>(user);

            return userProfile;
        }

        public bool Login(string nickname, string password)
        {
            if(nickname == null || password == null)
                throw new ArgumentException("No password or login fof sign in account");

            string passwordHash = _hasherPassword.GetHash(password);
            User user = _uow.Users.GetAll().FirstOrDefault(u => u.Nickname == nickname && u.PasswordHash == passwordHash);

            return user != null;
        }

        public bool Register(string nickname, string password, string email)
        {
            if(nickname == null || password == null || email == null)
                throw  new ArgumentException("Invalid input information for registration");

            User user = _uow.Users.GetAll().FirstOrDefault(u => u.Nickname == nickname || u.Email == email);

            if (user != null)
                return false;

            _uow.Users.Create(new User { Nickname = nickname, Email = email, PasswordHash = _hasherPassword.GetHash(password) });
            _uow.Users.Save();

            return true;
        }

        public IEnumerable<string> GetAllNicknames()
        {
            return _uow.Users.GetAll().Select(u => u.Nickname);
        }
    }
}
