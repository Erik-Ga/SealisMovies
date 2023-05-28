using System.Text.Json;

namespace SealisMovies.DAL
{
    public class CategoryManagerAPI
    {
        private static Uri BaseAddress = new Uri("https://sealisapi.azurewebsites.net/");

        public static async Task<List<Models.Category>> GetAllCategories()
        {
            List<Models.Category> categories = new List<Models.Category>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.GetAsync("API/Category");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<Models.Category>>(responseString);
                }
                return categories;
            }
        }


        public static async Task<Models.Category> GetCategory(int id)
        {
            Models.Category category = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.GetAsync("API/Category/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    category = JsonSerializer.Deserialize<Models.Category>(responseString);
                }
                return category;
            }
        }
        public static async Task DeleteCategory(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.DeleteAsync("API/Category/" + id);
            }
        }
        public static async Task SaveCategory(Models.Category category)
        {
            var cate = (await GetAllCategories()).Where(p => p.Id == category.Id).FirstOrDefault();

            if (cate != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = BaseAddress;
                    var json = JsonSerializer.Serialize(category);
                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("API/Category/" + cate.Id, httpContent);

                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = BaseAddress;

                    var json = JsonSerializer.Serialize(category);
                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("API/Category", httpContent);

                }
            }
        }
    }
}
