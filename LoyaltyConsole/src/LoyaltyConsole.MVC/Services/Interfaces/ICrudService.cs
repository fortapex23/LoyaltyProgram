namespace LoyaltyConsole.MVC.Services.Interfaces
{
    public interface ICrudService
    {
        Task<T> GetAsync<T>(string endpoint);
        Task CreateAsync<T>(string endpoint, T entity);
        Task CreateWithImageAsync<T>(string endpoint, T entity) where T : class;
        Task UpdateAsync<T>(string endpoint, T entity);
        Task DeleteAsync(string endpoint);
        Task UpdateWithImageAsync<T>(string endpoint, int id, T entity) where T : class;
    }
}
