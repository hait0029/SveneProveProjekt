
namespace SvenePrøveProjekt.Repositories
{
    public class CityRepo : ICityRepo
    {

        private DatabaseContext _context { get; set; }
        public CityRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<City> CreateCity(City newCity)
        {
            _context.City.Add(newCity);
            await _context.SaveChangesAsync();
            return newCity;
        }


        public async Task<List<City>> GetAllCities()
        {
            return await _context.City.ToListAsync();
        }

        public async Task<City> GetCityById(int cityId)
        {
            return await _context.City.FirstOrDefaultAsync(x => x.CityID == cityId);
        }

        public async Task<City> UpdateCity(City city, int cityId)
        {
            throw new NotImplementedException();
        }

        public async Task<City> DeleteCity(int cityId)
        {
            throw new NotImplementedException();

        }
    }
}
