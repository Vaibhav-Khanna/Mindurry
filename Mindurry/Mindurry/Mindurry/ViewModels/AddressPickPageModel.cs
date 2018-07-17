using Mindurry.Models;
using Mindurry.Services.Abstraction;
using Mindurry.Services.Implementation;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class AddressPickPageModel : BasePageModel
    {

        private readonly IPlaceService _placeService;
        private CancellationTokenSource _cancel;

        private ObservableCollection<PlaceLocation> _locations = new ObservableCollection<PlaceLocation>();
        public ObservableCollection<PlaceLocation> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                RaisePropertyChanged();
            }
        }

        private bool _isVisibleListView;
        public bool IsVisibleListView
        {

            get { return _isVisibleListView; }
            set
            {
                _isVisibleListView = value;
                RaisePropertyChanged();
            }
        }
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisePropertyChanged();
            }
        }

        public async void SelectAddress(string item)
        {
            await CoreMethods.PopPageModel(new Tuple<string>(item), true, true);
        }

        public Command CloseCommand => new Command(async (obj) =>
        {
            await CoreMethods.PopPageModel(true);
        });



        public ICommand ChosseMyLocationCommand
        {
            get
            {
                return new Command(async () =>
                {

                    try
                    {
                        await CoreMethods.PopPageModel(true, false, false);
                    }
                    catch (Exception ex)
                    {
                        await CoreMethods.DisplayAlert("Erreur", "Impossible d'effectuer l'opération. Merci de recommencer.", "OK");
                    }

                });

            }
        }

        public ICommand FindPlaceCommand => new Command<string>(ExecuteFindPlaceCommandAsync);

        private void ExecuteFindPlaceCommandAsync(string searchString)
        {
            if (searchString?.Length >= 3)
            {
                IsVisibleListView = true;
                PerformSearch();
            }
            else
            {
                IsVisibleListView = false;
                Locations.Clear();
            }
        }

        private void PerformSearch()
        {
            try
            {
                //cancel existing task if applicable
                if (_cancel != null && !_cancel.IsCancellationRequested)
                {
                    _cancel.Cancel();
                }

                //create new search task
                _cancel = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                FirePlacesSearch(_placeService.GetResult(SearchText), _cancel);
            }
            catch (TaskCanceledException tce)
            {
            }
        }

        private async void FirePlacesSearch(Task<WebServiceResults.Result> getResult, CancellationTokenSource onCancel)
        {

            try
            {
                WebServiceResults.Result result = await Task.Run(() => getResult, onCancel.Token);
                if (result != null)
                {
                    Locations.Clear();

                    foreach (var prediction in result.predictions.Where(newItem => Locations.All(oldItem => oldItem.Id != newItem.id)))
                    {

                        Locations.Add(new PlaceLocation()
                        {
                            Id = prediction.place_id,
                            Location = prediction.description
                        });

                    }
                }

            }

            catch (TaskCanceledException ex)
            {

            }



        }

        public ICommand PickPlaceCommand => new Command<PlaceLocation>((place) => { var mypage = ExecutePickPlaceCommandAsync(place); });


        private async Task ExecutePickPlaceCommandAsync(PlaceLocation place)
        {
            SearchText = place.Location;
            var position = await _placeService.GetResultDetail(place.Id);
            var _latitude = position.Result.Geometry.Location.Lat;
            var _longitude = position.Result.Geometry.Location.Lng;
            var _addressComponents = position.Result.AddressComponents;
            IsVisibleListView = false;

            var tuple = new Tuple<string, double, double, List<AddressComponent>>(SearchText, _latitude, _longitude, _addressComponents);
            await CoreMethods.PopPageModel(tuple, true, true);



        }

        public AddressPickPageModel()
        {
            _placeService = new PlaceService();
        }




    }
}


