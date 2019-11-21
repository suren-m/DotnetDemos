using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DemoApp.Services;
using DemoApp.ViewModels;

namespace DemoApp
{
    public partial class WebDemoCombinatorsAsync : Page
    {

        private readonly int CTS_TIMEOUT_DEFAULT = 60;  // This will still be ticking during debugging
        public WebDemoCombinatorsAsync()
        {         
            InitializeComponent();
        }

        private async void AllThreeBtn_Click(object sender, RoutedEventArgs e)
        { 
            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(CTS_TIMEOUT_DEFAULT));
            var ct = cts.Token;

            this.UKCountiesList1.ItemsSource = new string[] { "...Loading..." };
            this.UKCountiesList2.ItemsSource = new string[] { "...Loading..." };
            this.UKCountiesList3.ItemsSource = new string[] { "...Loading..." };

            var taskA = UKCountiesService.GetCountiesAsync($"{App.API_BASE_URL}/ukcounties?delay=3s", ct, captureContext: false);
            var taskB = UKCountiesService.GetCountiesAsync($"{App.API_BASE_URL}/ukcounties?delay=5s", ct, captureContext: false);
            var taskC = UKCountiesService.GetCountiesAsync($"{App.API_BASE_URL}/ukcounties", ct, captureContext: false);

            Task<IEnumerable<UKCounty>>[] tasks = new [] { taskA, taskB, taskC };

            var tasksMap = new Dictionary<int, ListView>();
            tasksMap.Add(taskA.Id, UKCountiesList1);
            tasksMap.Add(taskB.Id, UKCountiesList2);
            tasksMap.Add(taskC.Id, UKCountiesList3);
            
            foreach (var t in tasks)
            {
                var res = await t;
                tasksMap[t.Id].ItemsSource = res;
            }

        }

        private async void AllThreeBtnAnyOrder_Click(object sender, RoutedEventArgs e)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(CTS_TIMEOUT_DEFAULT));
            var ct = cts.Token;

            this.UKCountiesList1.ItemsSource = new string[] { "...Loading..." };
            this.UKCountiesList2.ItemsSource = new string[] { "...Loading..." };
            this.UKCountiesList3.ItemsSource = new string[] { "...Loading..." };

            var taskA = UKCountiesService.GetCountiesAsync($"{App.API_BASE_URL}/ukcounties?delay=3s", ct, captureContext: false);
            var taskB = UKCountiesService.GetCountiesAsync($"{App.API_BASE_URL}/ukcounties?delay=5s", ct, captureContext: false);
            var taskC = UKCountiesService.GetCountiesAsync($"{App.API_BASE_URL}/ukcounties", ct, captureContext: false);

            Task<IEnumerable<UKCounty>>[] tasks = new[] { taskA, taskB, taskC };

            var tasksMap = new Dictionary<int, ListView>();
            tasksMap.Add(taskA.Id, UKCountiesList1);
            tasksMap.Add(taskB.Id, UKCountiesList2);
            tasksMap.Add(taskC.Id, UKCountiesList3);

            async Task AwaitAndProcessAsync(Task<IEnumerable<UKCounty>> t)
            {
                var res = await t;
                tasksMap[t.Id].ItemsSource = res;
            }

            var query = tasks.Select(t => AwaitAndProcessAsync(t));
            Task[] processingTasks = query.ToArray();

            await Task.WhenAll(processingTasks);

        }
    }
}
