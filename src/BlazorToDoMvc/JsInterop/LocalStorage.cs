using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Text.Json;

namespace BlazorToDoMvc.JsInterop
{
    public class LocalStorage
    {
        private readonly IJSRuntime _jSRuntime;

        public LocalStorage(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task<T> SetItemAsync<T>(string key, T value)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            var item = await _jSRuntime.InvokeAsync<string>("setItemFromLocalStorage", key, serializedValue);
            return value;
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            var item = await _jSRuntime.InvokeAsync<string>("getItemFromLocalStorage", key);

            if (item == null)
                return default;

            return JsonSerializer.Deserialize<T>(item);
        }
    }
}
