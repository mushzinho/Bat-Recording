using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatRecording
{
    public partial class Contract : Form
    {
        public Contract()
        {
            InitializeComponent();
        }

        private void Contract_Load(object sender, EventArgs e)
        {

        }

        public bool ValidateStepOne()
        {

            bool hasError = false;
            bool hasSomeChecked = false;

            //Tipo Pessoa
            foreach (var radioButton in this.groupBoxTipoPessoa.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) hasSomeChecked = true;
           
            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;

   
            //Nascimento
            if (!tbNascimento.MaskFull) hasError = true;

            //Sexo
            foreach (var radioButton in this.groupBoxSexo.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) hasSomeChecked = true;

            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;

            //Vendedor
            if (string.IsNullOrWhiteSpace(tbVendedor.Text)) hasError = true;
            //Nome/Razao
            if (string.IsNullOrWhiteSpace(tbNomeRazao.Text) ) hasError = true;
            //email
            if (string.IsNullOrWhiteSpace(tbEmail.Text) ) hasError = true;
            //Fixo
            if (!tbTelefoneFixo.MaskCompleted) hasError = true;
            //Movel
            if (!tbTelefoneMovel.MaskCompleted) hasError = true;

            //Estado Civil
            foreach (var radioButton in this.gbEstadoCivil.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) hasSomeChecked = true;

            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;

            //CPF CNPJ
            if (string.IsNullOrWhiteSpace(tbCpfCnpj.Text)) hasError = true;
            //Rg//Inc
            if (string.IsNullOrWhiteSpace(tbRGIncsEstRNE.Text)) hasError = true;
            
            //TipoImovel
            foreach (var radioButton in this.gbTipoImovel.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) hasSomeChecked = true;

            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;

            //CEP
            if (!tbCep.MaskCompleted) hasError = true;
            //Endereço
            if (string.IsNullOrWhiteSpace(tbEndereco.Text)) hasError = true;
            //Bairro
            if (string.IsNullOrWhiteSpace(tbBairro.Text)) hasError = true;
            //Numero
            if (string.IsNullOrWhiteSpace(tbNumero.Text)) hasError = true;
            //Cidade
            if (string.IsNullOrWhiteSpace(tbCidade.Text)) hasError = true;
            //UF
            if (string.IsNullOrWhiteSpace(tbUF.Text)) hasError = true;



            return hasError;

        }

        public bool validateStepTwo()
        {
            bool hasError = false;
            bool hasSomeChecked = false;

            //FormaPagamento
            foreach (var radioButton in this.gbFormaPagamento.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) hasSomeChecked = true;

            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;


            //Modelo TV
            foreach (var radioButton in this.gbModeloTV.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) hasSomeChecked = true;

            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;

            return hasError;
        }

        private void btProximo_Click(object sender, EventArgs e)
        {
            if (ValidateStepOne())
            {
                MessageBox.Show(@"ERRO: Todos revise todos os campos com *");
            }
            else
            {
                tabControlContrato.SelectTab(tabPlanoPagamento);
            }
        }

        private void btFinalizar_Click(object sender, EventArgs e)
        {
            if (validateStepTwo())
            {
                MessageBox.Show(@"ERRO: Todos revise todos os campos com *");
            }
            else
            {
                //
            }
        }
    }
}
