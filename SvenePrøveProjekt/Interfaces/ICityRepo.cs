using SvenePrøveProjekt.Models;

namespace SvenePrøveProjekt.Interfaces
{
    public interface ICityRepo
    {
        public Task<List<City>> GetAllCities();
        public Task<City> GetCityById(int cityId);
        public Task<City> CreateCity(City city);
        public Task<City> UpdateCity(City city, int cityId);
        public Task<City> DeleteCity(int cityId);

    }
}
