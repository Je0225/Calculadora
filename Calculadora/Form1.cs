using System;
using System.Windows.Forms;

namespace Calculadora {

    public partial class Form1: Form {

        public Form1() {
            InitializeComponent();
        }
        // KEYPREVIEW: o keyPreview do formulário deve estar habilitado para receber os eventos que venham a partir do teclado

        private void btn0_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn0.Text}";
        }

        private void btnAbreParenteses_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btnAbreParenteses.Text}";
        }

        private void btnFechaParenteses_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btnFechaParenteses.Text}";
        }

        private void btn1_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn1.Text}";
        }

        private void btn2_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn2.Text}";
        }

        private void btn3_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn3.Text}";
        }

        private void btn4_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn4.Text}";
        }

        private void btn5_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn5.Text}";
        }

        private void btn6_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn6.Text}";
        }

        private void btn7_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn7.Text}";
        }

        private void btn8_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn8.Text}";
        }

        private void btn9_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btn9.Text}";
        }

        private void btnDivisao_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btnDivisao.Text}";
        }

        private void btnMultiplicacao_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btnMultiplicacao.Text}";
        }

        private void btnSubtracao_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btnSubtracao.Text}";
        }

        private void btnAdicao_Click(object sender, EventArgs e) {
            tbDisplay.Text = $"{tbDisplay.Text}{btnAdicao.Text}";
        }

        private void btnC_Click(object sender, EventArgs e) {
            tbDisplay.Clear();
        }

        private void btnIgual_Click(object sender, EventArgs e) {
            String equacao = tbDisplay.Text;
            if (equacao.Contains("=")) {
                Int32 IndiceSinalIgual = equacao.IndexOf("=");
                equacao = equacao.Remove(0, IndiceSinalIgual + 1);
                tbDisplay.Text = equacao;

            } else {
                if (equacao.Contains("(") && equacao.Contains(")")) {
                    tbDisplay.Text = $" {tbDisplay.Text} = {AvaliaOperacaoComParenteses(equacao)}";
                } else if ((equacao.Contains("+") || equacao.Contains("-")) && !equacao.Contains("*") && !equacao.Contains("/")) {
                    tbDisplay.Text = $"{tbDisplay.Text} = {AvaliaAdicaoESubtracao(equacao)}";
                } else if (equacao.Contains("/") || equacao.Contains("*")) {
                    tbDisplay.Text = $" {tbDisplay.Text} = {AvaliaDivisaoEMultiplicacao(equacao)}";
                }
            }
        }

        private String AvaliaOperacaoComParenteses(String equacao) {
            Int32 ixLastAbreParenteses = equacao.LastIndexOf("(");
            Int32 ixFirstFechaParentes = equacao.IndexOf(")",ixLastAbreParenteses);
            String equacaoParenteses = "";
            string result = "";

            if (Char.IsNumber(equacao[ixLastAbreParenteses - 1]) || equacao[ixLastAbreParenteses - 1] == ')') {
                equacao = equacao.Insert(ixLastAbreParenteses, "*");
            }
            
            ixLastAbreParenteses = equacao.LastIndexOf("(");
            ixFirstFechaParentes = equacao.IndexOf(")",ixLastAbreParenteses);

            if (ixFirstFechaParentes != equacao.Length -1) {
                if (Char.IsNumber(equacao[ixFirstFechaParentes + 1]) || equacao[ixFirstFechaParentes + 1] == '(') {
                    equacao = equacao.Insert(ixFirstFechaParentes + 1, "*");
                }
            }
            ixLastAbreParenteses = equacao.LastIndexOf("(");
            ixFirstFechaParentes = equacao.IndexOf(")", ixLastAbreParenteses) ;

            while (ixFirstFechaParentes != -1 && ixLastAbreParenteses != -1) {
                equacaoParenteses = equacao.Substring(ixLastAbreParenteses, ixFirstFechaParentes - ixLastAbreParenteses + 1);
                if ((equacaoParenteses.Contains("+") || equacaoParenteses.Contains("-")) && !equacaoParenteses.Contains("*") && !equacaoParenteses.Contains("/")) {
                    result = AvaliaAdicaoESubtracao(equacaoParenteses.Replace("(", "").Replace(")", ""));
                } else if (equacaoParenteses.Contains("/") || equacaoParenteses.Contains("*")) {
                    result = AvaliaDivisaoEMultiplicacao(equacaoParenteses.Replace("(", "").Replace(")", ""));
                }
                equacao = equacao.Replace(equacaoParenteses, result);
                ixLastAbreParenteses = equacao.LastIndexOf("(");
                ixFirstFechaParentes = equacao.IndexOf(")");
            }

            if ((equacao.Contains("+") || equacao.Contains("-")) && !equacao.Contains("*") && !equacao.Contains("/")) {
                result = AvaliaAdicaoESubtracao(equacao);
            } else if (equacao.Contains("/") || equacao.Contains("*")) {
                result = AvaliaDivisaoEMultiplicacao(equacao);
            }
            return result;
        }

        private int IndiceValido(String equacao) {
            int indiceOperadorVezes = equacao.IndexOf('*');
            int indiceOperadorDivisao = equacao.IndexOf('/');
            int min = Math.Min(indiceOperadorDivisao, indiceOperadorVezes);
            int max = Math.Max(indiceOperadorDivisao, indiceOperadorVezes);

            if (min != -1) {
                return min;
            }
            return max;
        }

        private String AvaliaDivisaoEMultiplicacao(String equacao) {
            string pedacoA = "";
            string pedacoB = "";
            String result = "";

            while (IndiceValido(equacao) != -1) {
                int indiceOperador = IndiceValido(equacao);

                for (int i = indiceOperador + 1; i < equacao.Length; i++) {
                    Char digito = equacao[i];
                    if (Char.IsNumber(digito) || digito == ',' || (pedacoB == "" && digito == '-')) {
                        pedacoB = $"{pedacoB}{equacao.Substring(i, 1)}";
                    } else {
                        break;
                    }
                }
                for (int i = indiceOperador - 1; i >= 0; i--) {
                    Char digito = equacao[i];
                    if (Char.IsNumber(digito) || digito == ',' || (pedacoA == "" && digito == '-')) {
                        pedacoA = $"{equacao.Substring(i, 1)}{pedacoA}";
                    } else {
                        break;
                    }
                }
                string operador = equacao.Substring(indiceOperador, 1);
                result = Calcula(operador, pedacoA, pedacoB).ToString();
                equacao = equacao.Replace($"{pedacoA}{operador}{pedacoB}", $"{result}");
                pedacoA = "";
                pedacoB = "";
            }

            if (equacao.Contains("-") || equacao.Contains("+")) {
                return AvaliaAdicaoESubtracao(equacao);
            }
            return result;
        }

        private String AvaliaAdicaoESubtracao(String equacao) {
            string pedacoA = "";
            string pedacoB = "";
            string operador = "";
            equacao = equacao.Replace("--", "+");
            for (int i = 0; i < equacao.Length; i++) {
                Char digito = equacao[i];
                if ((Char.IsNumber(digito) || digito == ',' || pedacoA == "") && operador == "") {
                    pedacoA += digito.ToString();
                } else if ((Char.IsNumber(digito) || digito == ',' || pedacoB == "") && pedacoA != "" && operador != "") {
                    pedacoB += digito.ToString();
                } else if (IsOperator(digito)) {
                    if (operador == "") {
                        operador = digito.ToString();
                    } else {
                        equacao = equacao.Substring(i);
                        pedacoA = Calcula(operador, pedacoA, pedacoB).ToString();
                        i = -1;
                        operador = "";
                        pedacoB = ""; 
                    }
                }
            }
            return Calcula(operador, pedacoA, pedacoB).ToString();
        }

        private Boolean IsOperator(Char digito) {
            return (digito == '+' || digito == '-' || digito == '/' || digito == '*');
        }

        private Decimal Calcula(string operador, string pedacoA, string pedacoB) {
            decimal resultado = 0;

            switch (operador) {
                case "+":
                    resultado = Convert.ToDecimal(pedacoA) + Convert.ToDecimal(pedacoB);
                    break;
                case "-":
                    resultado = Convert.ToDecimal(pedacoA) - Convert.ToDecimal(pedacoB);
                    break;
                case "*":
                    resultado = Convert.ToDecimal(pedacoA) * Convert.ToDecimal(pedacoB);
                    break;
                case "/":
                    resultado = Convert.ToDecimal(pedacoA) / Convert.ToDecimal(pedacoB);
                    break;
                default:
                    resultado = Convert.ToDecimal(pedacoA);
                    break;
            }
            return Math.Round(resultado, 2);
        }

        private void tbDisplay_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    btnIgual.PerformClick();
                    break;
                case Keys.Delete:
                    btnC.PerformClick();
                    break;

            }

        }

    }

}