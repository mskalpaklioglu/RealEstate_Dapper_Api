using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using Dapper;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async void CreateCategory(CreateCategoryDto categoryDto)
        {
            string query = "INSERT INTO Category (CategoryName,CategoryStatus) values (@categoryName,@categoryStatus)";
            var parameters = new DynamicParameters();

            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteCategory(int id)
        {
            string query = "DELETE FROM Category WHERE CategoryID=@categoryID";

            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", id);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            string query = "Select * From Category";

            using (var connection = _context.CreateConnection())
            {
                // Dönen veriyi ne ile eşleştirecek
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();
            }
        }





        public async Task<GetByIDCategoryDto> GetCategory(int id)
        {
            string query = "SELECT * FROM Category WHERE CategoryID=@CategoryID";

            var parameters = new DynamicParameters();

            parameters.Add("@CategoryID", id);

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<GetByIDCategoryDto>(query, parameters);
                return result;
            }

        }






        public async void UpdateCategory(UpdateCategoryDto categoryDto)
        {

            string query = @"UPDATE Category SET
                                CategoryName=@categoryName,
                                CategoryStatus=@categoryStatus
                            where CategoryID=@categoryID";

            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", categoryDto.CategoryStatus);
            parameters.Add("@categoryID", categoryDto.CategoryID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        Task<List<ResultCategoryDto>> ICategoryRepository.GetAllCategoryAsync()
        {
            throw new NotImplementedException();
        }
    }
}

