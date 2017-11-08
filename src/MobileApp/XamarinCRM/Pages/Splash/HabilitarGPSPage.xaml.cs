
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using XamarinCRM.Services;
using XamarinCRM.Statics;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Splash;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin;

namespace XamarinCRM.Pages.Splash 
{
    public partial class HabilitarGPSPage : HabilitarGPSPageXaml
    {
        readonly IAuthenticationService _AuthenticationService;

        public HabilitarGPSPage()
        {
            InitializeComponent();

            BindingContext = new SplashViewModel();
            _AuthenticationService = DependencyService.Get<IAuthenticationService>();

            HabilitarButton.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    NumberOfTapsRequired = 1, 
                    Command = new Command(HabilitarButtonTapped) 
                });

            EnderecoManualButton.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    NumberOfTapsRequired = 1, 
                    Command = new Command(EnderecoManualButtonTapped) 
                });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // fetch the demo credentials
            await ViewModel.LoadDemoCredentials();

            // pause for a moment before animations
            await Task.Delay(App.AnimationSpeed);

            // Sequentially animate the login buttons. ScaleTo() makes them "grow" from a singularity to the full button size.
            await HabilitarButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
            await EnderecoManualButton.ScaleTo(1, (uint)App.AnimationSpeed, Easing.SinIn);
        }

        async void HabilitarButtonTapped()
        {
            _AuthenticationService.BypassAuthentication();
            MessagingCenter.Send(this, MessagingServiceConstants.AUTHENTICATED);
            App.GoToRoot();
        }

        async void EnderecoManualButtonTapped()
        {
            _AuthenticationService.BypassAuthentication();
            MessagingCenter.Send(this, MessagingServiceConstants.AUTHENTICATED);

            App.GoToRoot();
        }
    }

    /// <summary>
    /// This class definition just gives us a way to reference ModelBoundContentPage<T> in the XAML of this Page.
    /// </summary>
    public abstract class HabilitarGPSPageXaml : ModelBoundContentPage<SplashViewModel>
    {
    }
}

