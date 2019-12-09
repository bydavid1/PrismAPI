using Newtonsoft.Json;
using PokeApi.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PokeApi.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _nombre;
        private string _url;
        private ObservableCollection<Pokemon> items;
        private IPageDialogService _dialogService;


        public string nombre
        {
            get { return _nombre; }
            set { SetProperty(ref _nombre, value); }
        }

        public string url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }

        public ObservableCollection<Pokemon> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            _dialogService = dialogService;
            GetList();
        }

        private async void GetList()
        {
            try
            {
                var url = "https://pokeapi.co/api/v2/pokemon";
                HttpClient client = new HttpClient();
                HttpResponseMessage connect = await client.GetAsync(url);

                if (connect.StatusCode == HttpStatusCode.OK)
                {
                    var response = await client.GetStringAsync(url);
                    var lista = JsonConvert.DeserializeObject<List<Pokemon>>(response);
                    Items = new ObservableCollection<Pokemon>(lista);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Error", "Ocurrio un error", "ok");
                }

            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Error", "Excepcion: " + ex, "ok");
            }
        }
    }
}
