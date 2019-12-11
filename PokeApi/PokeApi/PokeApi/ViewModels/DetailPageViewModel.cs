using GalaSoft.MvvmLight.Command;
using PokeApi.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PokeApi.ViewModels
{
    public class DetailPageViewModel : BindableBase
    {
             private static pkmn pokemon;
        private DetailPageViewModel instance;
        public DetailPageViewModel current
        {
            get
            {
                if (instance == null)
                {
                    instance = new DetailPageViewModel();
                }
                return instance;
            }
        }
        public pkmn Pokemon
        {
            get
            {
                return pokemon;
            }
            set
            {
                SetProperty(ref pokemon, value);
            }
        }
        public DetailPageViewModel()
        {
            LoadPokemon();
        }
        public void LoadPokemon()
        {
            Pokemon = DetailPageViewModel.pokemon;
        }
        public ICommand executeThis
        {
            get
            {
                return new RelayCommand(LoadPokemon);
            }
        }
        public static void SetPokemon(pkmn pokemon)
        {
            DetailPageViewModel.pokemon = pokemon;
        }
    }
}