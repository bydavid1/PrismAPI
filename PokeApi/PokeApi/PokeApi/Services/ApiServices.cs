using Newtonsoft.Json;
using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Services
{
    class ApiService
    {
        HttpClient cliente;
        public ApiService()
        {
            cliente = new HttpClient();
        }
        public async Task<ObservableCollection<pkmn>> GetPkmns()
        {
            var content = await cliente.GetAsync("https://pokeapi.co/api/v2/pokemon");
            var json = await content.Content.ReadAsStringAsync();
            pkmResponse PR = JsonConvert.DeserializeObject<pkmResponse>(json);
            return await pkmnInfo(PR);
        }
        public async Task<ObservableCollection<pkmn>> pkmnInfo(pkmResponse pkmResp)
        {
            ObservableCollection<pkmn> Pokemon = new ObservableCollection<pkmn>();
            pkmn poke;
            for (int i = 0; i < pkmResp.results.Count; i++)
            {
                poke = new pkmn();
                var content = await cliente.GetAsync(pkmResp.results[i].url);
                var json = await content.Content.ReadAsStringAsync();
                poke = JsonConvert.DeserializeObject<pkmn>(json);
                Pokemon.Add(poke);
            }
            return Pokemon;
        }
    }
}
