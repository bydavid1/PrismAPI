using Newtonsoft.Json;
using PokeApi.Models;
using PokeApi.Services;
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

        private bool isRefreshing;
        private ObservableCollection<pkmn> items;
        private IPageDialogService _dialogService;
        private ApiService api;
        private pkmn pokemon;

        public ObservableCollection<pkmn> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public pkmn Pokemon
        {
            get { return pokemon; }
            set { SetProperty(ref pokemon, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            _dialogService = dialogService;
            api = new ApiService();
            LoadData();
        }

        public async void LoadData()
        {
            IsRefreshing = true;
            Items = await api.GetPkmns();
            IsRefreshing = false;
        }
    }
}
