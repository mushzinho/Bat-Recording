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
        public string TextToBeSaved;
        private RadioButton _rbTipoPessoa;
        private RadioButton _rbSexo;
        private RadioButton _rbEstadoCivil;
        private RadioButton _rbTipoImovel;
        private RadioButton _rbFormaPagamento;
        private RadioButton _rbModeloTv;

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
                if (radioButton.Checked)
                {
                    hasSomeChecked = true;
                    _rbTipoPessoa = radioButton;
                    break;
                }
           
            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;

   
            //Nascimento
            if (!tbNascimento.MaskFull) hasError = true;

            //Sexo
            foreach (var radioButton in this.groupBoxSexo.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    hasSomeChecked = true;
                    _rbSexo = radioButton;
                    break;
                }

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
                if (radioButton.Checked)
                {
                    hasSomeChecked = true;
                    _rbEstadoCivil = radioButton;
                    break;
                }

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
                if (radioButton.Checked)
                {
                    hasSomeChecked = true;
                    _rbTipoImovel = radioButton;
                    break;
                }

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



            return !hasError;

        }

        public bool ValidateStepTwo()
        {
            bool hasError = false;
            bool hasSomeChecked = false;

            //FormaPagamento
            foreach (var radioButton in this.gbFormaPagamento.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    hasSomeChecked = true;
                    _rbFormaPagamento = radioButton;
                    break;
                }

            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;


            //Modelo TV
            foreach (var radioButton in this.gbModeloTV.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    hasSomeChecked = true;
                    _rbModeloTv = radioButton;
                    break;
                }

            }
            if (!hasSomeChecked) hasError = true;
            hasSomeChecked = false;

            return !hasError;
        }

        private void CreateTextString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Vendedor: " + tbVendedor.Text);
            sb.AppendLine("Tipo de Pessoa: " + _rbTipoPessoa.Name);
            sb.AppendLine("Nascimento: " + tbNascimento.Text);
            sb.AppendLine("Sexo: " + _rbSexo.Name);
            sb.AppendLine("Nome / Razão: " + tbNomeRazao.Text);
            sb.AppendLine("Email: " + tbEmail.Text);
            sb.AppendLine("Telefone Fixo: " + tbTelefoneFixo.Text);
            sb.AppendLine("Telefone Móvel: " + tbTelefoneMovel.Text);
            sb.AppendLine("Estado Civel: " + _rbEstadoCivil.Name);
            sb.AppendLine("CPF / CNPJ: " + tbCpfCnpj.Text);
            sb.AppendLine("RG / Insc Est / RNE: " + tbRGIncsEstRNE.Text);
            sb.AppendLine("Tipo do Imóvel: " + _rbTipoImovel.Name);
            sb.AppendLine("Nome do Edifício: " + tbNomeEdificio.Text);
            sb.AppendLine("CEP: " + tbCep.Text);
            sb.AppendLine("Endereço: " + tbEndereco.Text);
            sb.AppendLine("Bairro: " + tbBairro.Text);
            sb.AppendLine("Complemento: " + tbComplemento.Text);
            sb.AppendLine("Condomínio: " + tbCondominio.Text);
            sb.AppendLine("Cidade: " + tbCidade.Text);
            sb.AppendLine("Pto. de Referência: " + tbReferencia.Text);
            sb.AppendLine("Pré-Pago: " + tbPrePago.Text);
            sb.AppendLine("Qtd Pré-Pago: " + tbQtdPre.Text);
            sb.AppendLine("Pós-Pago: " + tbPosPago.Text);
            sb.AppendLine("Qtd Pós-Pago: " + tbQtdPos.Text);
            sb.AppendLine("Qtd Pós-Pago: " + tbQtdPos.Text);

            foreach (var radioButton in this.gbBandaLarga.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    sb.AppendLine("Banda Larga: " + radioButton.Name);
                }

            }
            sb.AppendLine("Modelo TV: " + _rbModeloTv.Name);
            sb.AppendLine("Forma de Pagamento: " + _rbFormaPagamento.Name);
            sb.AppendLine("Número do Cartão: " + tbNumeroCartao.Text);
            sb.AppendLine("Validade: " + tbValidade.Text);
            sb.AppendLine("Agencia: " + tbAgencia.Text);
            sb.AppendLine("Conta: " + tbConta.Text);
            sb.AppendLine("Banco: " + tbBanco.Text);
            sb.AppendLine("Taxa de Adesão / Inst: " + tbTaxaAdesao.Text);
            sb.AppendLine("Parcelas: " + tbParcelas.Text);
            sb.AppendLine("Data da Proposta: " + tbDataProposta.Text);

            this.TextToBeSaved = sb.ToString();



        }
        private void btProximo_Click(object sender, EventArgs e)
        {
            if (ValidateStepOne())
            {
                tabControlContrato.SelectTab(tabPlanoPagamento);
                
            }
            else
            {
                MessageBox.Show(@"ERRO: Revise todos os campos com *");
            }
        }

        private void btFinalizar_Click(object sender, EventArgs e)
        {
            if (ValidateStepTwo() && ValidateStepTwo() )
            {
                this.CreateTextString();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(@"ERRO: Revise todos os campos com *");
            }
        }
    }
}
