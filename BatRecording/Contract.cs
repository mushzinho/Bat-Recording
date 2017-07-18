using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatRecording
{
    public partial class Contract : Form
    {
        public string TextToBeSaved;
        public string ClientName;
        public string CpfCnpj;
        private RadioButton _rbTipoPessoa;
        private RadioButton _rbSexo;
        private RadioButton _rbEstadoCivil;
        private RadioButton _rbTipoImovel;
        private RadioButton _rbFormaPagamento;
        private RadioButton _rbModeloTv;
        private RadioButton _rbBandaLarga;

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
                    _rbFormaPagamento = radioButton;
                    break;
                }

            }


            //Banda Larga
            foreach (var radioButton in this.gbBandaLarga.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    _rbBandaLarga = radioButton;
                    break;
                }

            }

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

            //Cartao Vazio
            if (_rbFormaPagamento != null)
            {

                if (_rbFormaPagamento.Name == "rbCartaoCredito" && (string.IsNullOrEmpty(tbNumeroCartao.Text) || !tbValidade.MaskCompleted ))
                {
                    MessageBox.Show(@"Número do Cartão e validade obrigatórios para a opção selecionada.");
                    hasError = true;
                }


                //Deb em Conta

                if (_rbFormaPagamento.Name == "rbDebitoCc" &&
                    (string.IsNullOrEmpty(tbAgencia.Text) || string.IsNullOrEmpty(tbConta.Text) ||
                     string.IsNullOrEmpty(tbBanco.Text)))
                {
                    hasError = true;
                    MessageBox.Show(@"Agência, Conta e Banco obrigatórios para a opção selecionada.");
                }


            }
            else
            {
                MessageBox.Show(@"Escolha uma forma de pagamento.");
                hasError = true;
            }

            if (_rbBandaLarga == null)
            {
                if (string.IsNullOrEmpty(tbPrePago.Text) && string.IsNullOrEmpty(tbPosPago.Text))
                {
                    MessageBox.Show(@"Você deve informar as opções de plano.");
                    hasError = true;
                }

            }

            return !hasError;
        }

        private void CreateTextString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Vendedor: " + tbVendedor.Text);
            sb.AppendLine("Tipo de Pessoa: " + _rbTipoPessoa.Text);
            sb.AppendLine("Nascimento: " + tbNascimento.Text);
            sb.AppendLine("Sexo: " + _rbSexo.Text);
            sb.AppendLine("Nome / Razão: " + tbNomeRazao.Text);
            sb.AppendLine("Email: " + tbEmail.Text);
            sb.AppendLine("Telefone Fixo: " + tbTelefoneFixo.Text);
            sb.AppendLine("Telefone Móvel: " + tbTelefoneMovel.Text);
            sb.AppendLine("Estado Civel: " + _rbEstadoCivil.Text);
            sb.AppendLine("CPF / CNPJ: " + tbCpfCnpj.Text);
            sb.AppendLine("RG / Insc Est / RNE: " + tbRGIncsEstRNE.Text);
            sb.AppendLine("Tipo do Imóvel: " + _rbTipoImovel.Text);
            sb.AppendLine("Nome do Edifício: " + tbNomeEdificio.Text);
            sb.AppendLine("CEP: " + tbCep.Text);
            sb.AppendLine("Endereço: " + tbEndereco.Text);
            sb.AppendLine("Bairro: " + tbBairro.Text);
            sb.AppendLine("N°: " + tbNumero.Text);
            sb.AppendLine("Complemento: " + tbComplemento.Text);
            sb.AppendLine("Condomínio: " + tbCondominio.Text);
            sb.AppendLine("Cidade: " + tbCidade.Text);
            sb.AppendLine("Pto. de Referência: " + tbReferencia.Text);
            sb.AppendLine("UF: " + tbUF.Text);
            sb.AppendLine("Pré-Pago: " + tbPrePago.Text);
            sb.AppendLine("Qtd Pré-Pago: " + tbQtdPre.Text);
            sb.AppendLine("Pós-Pago: " + tbPosPago.Text);
            sb.AppendLine("Qtd Pós-Pago: " + tbQtdPos.Text);
            if (_rbBandaLarga != null)
            {
                sb.AppendLine("Banda Larga: " + _rbBandaLarga.Text);
            }
            else
            {
                sb.AppendLine("Banda Larga: ");
            }
            sb.AppendLine("Modelo TV: " + _rbModeloTv.Text);
            sb.AppendLine("Forma de Pagamento: " + _rbFormaPagamento.Text);
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
                MessageBox.Show(@"ERRO: Revise todos os campos com * ");
            }
        }

        private void btFinalizar_Click(object sender, EventArgs e)
        {
            if (ValidateStepTwo() && ValidateStepTwo() )
            {
                this.CreateTextString();
                this.ClientName = tbNomeRazao.Text;
                this.CpfCnpj = tbCpfCnpj.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(@"ERRO: Revise todos os campos com * e as mensagens de erro.");
            }
        }


        private void tbNomeRazao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys) e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void tbCpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space || e.KeyChar == '.' || e.KeyChar == '-') return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void tbRGIncsEstRNE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space || e.KeyChar == '.') return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void tbNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back ) return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void tbEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbBairro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbComplemento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbCondominio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbNomeEdificio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void tbQtdPre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void tbQtdPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void tbNumeroCartao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = char.IsLetter(e.KeyChar);
        }

        private void tbAgencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = char.IsLetter(e.KeyChar);
        }

        private void tbConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = char.IsLetter(e.KeyChar);
        }

        private void tbBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = char.IsDigit(e.KeyChar);
        }

        private void tbParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Back || (Keys)e.KeyChar == Keys.Space) return;
            e.Handled = !char.IsDigit(e.KeyChar);
        }
    }
}
