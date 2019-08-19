using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1_ConcultaCEP.Servico;
using App1_ConcultaCEP.Servico.Modelo;


namespace App1_ConcultaCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BtnBuscar.Clicked += BuscarCEP;
                
        }
        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = Cep.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        LblResultado.Text = string.Format("Endereço: {0}, {1}\n {2} - {3}", end.logradouro, end.bairro, end.localidade, end.uf);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o cep informado: " + cep, "Ok");
                    }
                    
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "Ok");
                }
            }
        }
        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if(cep.Length != 8)
            {
                //erro
                DisplayAlert("ERRO", "CEP inválido \nO CEP deve conter 8 caracteres.", "Ok");
                valido = false;
            }
            int NovoCep = 0;
            if(!int.TryParse(cep, out NovoCep))
            {
                //erro
                DisplayAlert("ERRO", "CEP inválido \nO CEP deve conter apenas números.", "Ok");
                valido = false;
            }
            return valido;
        }
    }
}
