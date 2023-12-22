using Pon.Site.Net.Web.Models;

namespace Pon.Site.Net.Web.Services
{
    public interface IToDoService
    {
        public Task<Item> Add(Item todo);
        public Task<List<Item>> Get();
        public Task<Item?> Get(Guid id);
        public Task<bool> Delete(Guid id);
    }
}
