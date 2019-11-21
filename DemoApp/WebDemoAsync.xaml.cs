using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DemoApp.Services;
using DemoApp.ViewModels;

namespace DemoApp
{
    public partial class WebDemoAsync : Page
    {

        private readonly int CTS_TIMEOUT_DEFAULT = 60;  // This will still be ticking during debugging
        public WebDemoAsync()
        {         
            InitializeComponent();
        }

        private async void UKCountiesBtn_Click(object sender, RoutedEventArgs e)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(CTS_TIMEOUT_DEFAULT)); 
            var ct = cts.Token;

            this.UKCountiesList.ItemsSource = new string[] { "...loading..." };
            var data = await UKCountiesService.GetCountiesAsync($"{App.API_BASE_URL}/ukcounties?delay=3s", ct, captureContext: false);

            this.UKCountiesList.ItemsSource = data;
        }

        private async void CountriesBtn_Click(object sender, RoutedEventArgs e)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(CTS_TIMEOUT_DEFAULT));
            var ct = cts.Token;

            this.CountriesList.ItemsSource = new string[] { "...loading..." };
            var data = await CountriesService.GetCountriesAsync($"{App.API_BASE_URL}/countries?delay=5s", ct, captureContext: false);

            this.CountriesList.ItemsSource = data;
        }

        private async void CarsBtn_Click(object sender, RoutedEventArgs e)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(CTS_TIMEOUT_DEFAULT)); 
            var ct = cts.Token;

            this.CarsList.ItemsSource = new string[] { "...loading..." };
            var data = await CarsService.GetCarsAsync($"{App.API_BASE_URL}/cars", ct , captureContext: false );

            this.CarsList.ItemsSource = data;
        }
    }
}
