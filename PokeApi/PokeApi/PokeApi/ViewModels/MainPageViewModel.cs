using GalaSoft.MvvmLight.Command;
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
using System.Windows.Input;

namespace PokeApi.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        private bool isRefreshing;
        private ObservableCollection<pkmn> items;
        private IPageDialogService _dialogService;
        INavigationService _navigationService;
        private ApiService api;
        private pkmn pokemon;
        private string img;

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

        public string Img
        {
            get { return img; }
            set { SetProperty(ref img, value); }
        }


        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            _dialogService = dialogService;
            _navigationService = navigationService;
            api = new ApiService();
            LoadData();
        }

        public async void LoadData()
        {
            IsRefreshing = true;
            Items = await api.GetPkmns();
            IsRefreshing = false;
        }

        public ICommand OnTapped
        {
            get
            {
                return new RelayCommand(OpenMenu);
            }
        }
        public void OpenMenu()
        {
             DetailPageViewModel.SetPokemon(Pokemon);
            _navigationService.NavigateAsync("DetailPage");
        }
    }
}
