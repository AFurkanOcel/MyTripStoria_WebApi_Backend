using Contracts.UserDtos;
using Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;

        public UserService(IUserRepository userRepository, ICountryRepository countryRepository, ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var country = await _countryRepository.GetByIdAsync(user.CountryId);
                var city = await _cityRepository.GetByIdAsync(user.CityId);
                userDtos.Add(new UserDto
                {
                    Id = user.UserID,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Age = user.Age,
                    CountryId = user.CountryId,
                    CityId = user.CityId,
                    CountryName = country?.Name,
                    CityName = city?.Name,
                    Address = user.Address,
                    Budget = user.Budget,
                    IsPremium = user.IsPremium
                });
            }

            return userDtos;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var country = await _countryRepository.GetByIdAsync(user.CountryId);
            var city = await _cityRepository.GetByIdAsync(user.CityId);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.UserID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                CountryId = user.CountryId,
                CityId = user.CityId,
                CountryName = country?.Name,
                CityName = city?.Name,
                Address = user.Address,
                Budget = user.Budget,
                IsPremium = user.IsPremium
            };
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            var country = await _countryRepository.GetByIdAsync(user.CountryId);
            var city = await _cityRepository.GetByIdAsync(user.CityId);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.UserID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                CountryId = user.CountryId,
                CityId = user.CityId,
                CountryName = country?.Name,
                CityName = city?.Name,
                Address = user.Address,
                Budget = user.Budget,
                IsPremium = user.IsPremium
            };
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            var country = await _countryRepository.GetByIdAsync(user.CountryId);
            var city = await _cityRepository.GetByIdAsync(user.CityId);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.UserID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                CountryId = user.CountryId,
                CityId = user.CityId,
                CountryName = country?.Name,
                CityName = city?.Name,
                Address = user.Address,
                Budget = user.Budget,
                IsPremium = user.IsPremium
            };
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteAsync(userId);
        }
    }
}

