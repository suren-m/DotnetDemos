using DemoApp.Services;
using DemoApp.Utils;
using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DemoApp
{
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            this.StatusBlock.Text = $"Page Created by UI Thread Id: {Thread.CurrentThread.ManagedThreadId}";
        }
                
        private async void DeadlockBtn_Click(object sender, RoutedEventArgs e)
        {            
            this.StatusBlock.Text = "Executing DeadlockBtn_Click";

            static async Task WaitAsync()
            {
                // This await will capture the current context. (configure await = true by default)
                await Task.Delay(TimeSpan.FromSeconds(2) ); 
                
                // ... and will attempt to resume the method here in that context.
            }

            Task task = WaitAsync();

            // Synchronously block (UI Thread!), waiting for the async method to complete.
            await task;

            // This will never be executed
            this.StatusBlock.Text = "Finished Executing DeadlockBtn_Click";

            // See: https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
        }

        //private void MyBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    // See: https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html

        //    this.StatusBlock.Text = "Beginning Execution";

        //    static async Task WaitAsync()
        //    {              
        //        await Task.Delay(TimeSpan.FromSeconds(1));              
        //    }

        //    Task task = WaitAsync();
                        
        //    task.Wait();

        //    this.StatusBlock.Text = "Finishing Execution";
            
        //}

        private void WebAPIDemoAsyncBtn_Click(object sender, RoutedEventArgs e)
        {
            WebDemoAsync page = new WebDemoAsync();
            this.NavigationService.Navigate(page);
        }

        private async void RunTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            this.StatusBlock.Text = "Executing RunTaskBtn_Click";
            async Task<int> DelayAndReturnThreadIdAsync()
            {
                await Task.Delay(2000).ConfigureAwait(false);
                return Thread.CurrentThread.ManagedThreadId;
            }

            var t = Task.Run<int>(async () =>
            {
                return await DelayAndReturnThreadIdAsync();
            }).ConfigureAwait(true);

            var res = await t;
            this.StatusBlock.Text = $"Finished Executing RunTaskBtn_Click. Task was run by Thread: {res}";
        }
        private async void StartNewTaskBtn_Click(object sender, RoutedEventArgs e)
        {

            Func<object, int> startFunc = (obj) =>
            {
                
                return Thread.CurrentThread.ManagedThreadId;
            };

            var t = Task.Factory.StartNew<int>(startFunc, TaskScheduler.FromCurrentSynchronizationContext());

            var res = await t;
            this.StatusBlock.Text = $"Finished Executing StartNewTaskBtn_Click. Task was run by Thread: {res}";
        }

        private void WebAPIDemoAsyncCombinatorsBtn_Click(object sender, RoutedEventArgs e)
        {
            WebDemoCombinatorsAsync page = new WebDemoCombinatorsAsync();
            this.NavigationService.Navigate(page);
        }

        private async void YieldDemoBtn_Click(object sender, RoutedEventArgs e)
        {
            var i = 0;
            foreach (var item in YieldService.YieldSha256Hash(100))
            {
                i++;
                this.StatusBlock.Text = $"Hash of {i} - {item}";
                await Task.Delay(1000);
            }
        }

        private async void IAsyncEnumerable_Click(object sender, RoutedEventArgs e)
        {
            var i = 0;
            await foreach (var item in YieldService.YieldSha256HashAsync(100))
            {
                i++;
                this.StatusBlock.Text = $"Hash of {i} - {item}";                
            }
        }

        private async void CancellationDemoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.StatusBlock.Text = "Exceuting CancellationDemoBtn_Click";

                await CancellationDemo();

                this.StatusBlock.Text = "CancellationDemoBtn_Click Finished Execution";
            }
            catch (Exception ex)
            {
                this.StatusBlock.Text = $"Task Cancelled. {ex}";
            }
        }

        private async Task CancellationDemo()
        {
            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
                var ct = cts.Token;
                //cts.Cancel();

                var data = await CarsService.GetCarsAsync($"{App.API_BASE_URL}/cars?delay=5s", ct, captureContext: false);

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void VoidExceptionDemo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.StatusBlock.Text = "Started VoidExceptionDemo_Click";
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                var ct = cts.Token;

                await DoSomethingAsync(ct);

                this.StatusBlock.Text = "Finished Executing VoidExceptionDemo_Click";
            }
            catch (Exception ex)
            {
                TraceUtil.PrintThreadInfo("Exception from Async Method", Thread.CurrentThread);
                this.StatusBlock.Text = $"Task Cancelled Exception. {ex}";
            }
        }

        // Exceptions get attached to a Task
        // If method is void, then exceptions can't be attached.        
        private async Task DoSomethingAsync(CancellationToken ct)
        {
            try
            {
                // should be awaited
                var _ = Task.Run(async () =>
                {
                    // something that takes long and will get cancelled
                    await Task.Delay(TimeSpan.FromSeconds(5), ct);   
                    
                }, ct);

                await _;
            }
            catch (Exception ex)
            {
                TraceUtil.PrintThreadInfo("Exception in DoSomethingAsync", Thread.CurrentThread);
                throw ex;
            }
        }

        private async void ParentAttachDemoBtn_Click(object sender, RoutedEventArgs e)
        {
            var status = string.Empty;

            var parent = Task.Factory.StartNew(() => {
                status = $"Outer task executing - ";
                var child = Task.Factory.StartNew(() =>
                {
                    status = $"{status} Nested task executing - ";
                    Thread.SpinWait(500000);
                    status = $"{status} Nested task Completed - ";
                }, TaskCreationOptions.AttachedToParent);

                //var child = Task.Run(() => {
                //    status = $"{status} Nested task executing - ";
                //    Thread.SpinWait(500000);
                //    status = $"{status} Nested task Completed - ";
                //});
            });

            await parent;
            status = $"{status} Outer task Completed";
            this.StatusBlock.Text = status;
        }        

        private async void PipelineDemoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.StatusBlock.Text = "Executing Pipeline";

            static async Task<int> delayAndReturn(int seconds)
            {
                await Task.Delay(TimeSpan.FromSeconds(seconds));
                return seconds; 
            }

            var antecedent = Task.Run(async () => await delayAndReturn(2));

            var followTask = await antecedent.ContinueWith<Task<string>>(async (ant) =>
            {
                var ant_time = await ant;
                var follow_time = await delayAndReturn(1);

                var total_time = ant_time + follow_time;

                var res = $"Pipeline ran for {total_time} seconds. Antecdent - {ant_time}s. FollowUpTask - {follow_time}s";
                return res;
            });

            var statusText = await followTask;
            this.StatusBlock.Text = statusText;
        }

        
    }
}
