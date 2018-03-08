using AutoMapper;
using WhatsNearbyApi.Entities;
using WhatsNearbyApi.Interfaces;
using WhatsNearbyApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WhatsNearbyApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private WhatsNearbyApiContext _context;

        public UserRepository(WhatsNearbyApiContext context)
        {
            _context = context;
        }

        public IEnumerable<CustomUserDto> GetUsers()
        {
            var userEntities = _context.CustomUsers.ToList();

            var users = Mapper.Map<IEnumerable<CustomUserDto>>(userEntities);

            return users;
        }

        public CustomUserDto GetUserByUsername(string username)
        {
            var userEntity = _context.CustomUsers.Where(o => o.UserName == username).FirstOrDefault();

            var user = Mapper.Map<CustomUserDto>(userEntity);

            return user;
        }
    }
}
