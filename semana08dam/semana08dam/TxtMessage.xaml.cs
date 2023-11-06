using semana08dam.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace semana08dam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TxtMessage : ContentPage
    {
        public TxtMessage()
        {
            InitializeComponent();
        }

        private void btnSpeak_Clicked(object sender, EventArgs e)
        {

            DependencyService.Get<ITextToSpeech>().Speak(txtMessage.Text);

        }
    }
}