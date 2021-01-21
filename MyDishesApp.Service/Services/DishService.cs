using AutoMapper;
using Microsoft.Extensions.Logging;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Repositories.Interfaces;
using MyDishesApp.Service.Service.Interfaces;
using MyDishesApp.Service.Dtos;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MyDishesApp.Service.Service
{
    public class DishService : IDishService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IDishRepository _dishRepository;
        private readonly IIngredientRepository _ingredientRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="DishService" />
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="dishRepository">The dish repository</param>
        /// <param name="ingredientRepository">The ingredient repository</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dishRepository" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ingredientRepository" /> is null.</exception>
        public DishService(ILogger<DishService> logger, IMapper mapper, IDishRepository dishRepository, IIngredientRepository ingredientRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
            _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        }

        public async Task<IEnumerable<DishDto>> GetDishListAsync()
        {
            var dishEntities = await _dishRepository.GetDishesAsync();
            return _mapper.Map<IEnumerable<DishDto>>(dishEntities);
        }


    }
}
