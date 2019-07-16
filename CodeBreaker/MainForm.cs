using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeBreaker
{
    public partial class MainForm : Form
    {
        private BigInteger? _publicKey;
        private BigInteger? _privateKey;
        private BigInteger? _n;
        private int _size = 512;
        public MainForm()
        {
            InitializeComponent();
        }

        private async void UploadButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var result = uploadFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            using (var reader = new StreamReader(uploadFileDialog.OpenFile()))
            {
                textArea.Text = await reader.ReadToEndAsync();
            }

            var length = textArea.Text.Length;
            var exp = 9;
            while (Math.Pow(2, exp) / 8 < length) exp++;
            _size = (int)Math.Pow(2, exp);
            EnableButtons();
        }

        private async void EncryptButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var input = textArea.Text;
            var inputInt = new BigInteger(Encoding.ASCII.GetBytes(input));
            var output = await Task<BigInteger>.Factory
                .StartNew(() => RSA.RSAEncrypt(inputInt, _publicKey, _n));
            textArea.Text = output+"";
            EnableButtons();
        }
        private async void DecryptButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var input = textArea.Text;
            var inputInt = BigInteger.Parse(input);
            var output = await Task<BigInteger>.Factory
                .StartNew(() => RSA.RSADecrypt(inputInt, _privateKey, _n));
            textArea.Text = Encoding.ASCII.GetString(output.ToByteArray());
            EnableButtons();
        }

        private async void GenerateButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            generateButton.Enabled = false;
            var keys = await Task<Tuple<BigInteger, BigInteger, BigInteger,BigInteger>>.Factory
                .StartNew(() => RSA.GenerateKeys(_size,true));

            _publicKey = keys.Item1;
            _privateKey = keys.Item2;
            _n = keys.Item3;

            publicKeyTextBox.Text = _publicKey + "";
            privateKeyTextBox.Text = _privateKey + "";
            nTextBox.Text = _n + "";

            EnableButtons();
        }

        private void DisableButtons()
        {
            generateButton.Enabled = false;
            encryptButton.Enabled = false;
            decryptButton.Enabled = false;
            attackButton.Enabled = false;
            uploadButton.Enabled = false;
            enterKeysButton.Enabled = false;
        }

        private void EnableButtons()
        {
            if(textArea.Text != "")
                generateButton.Enabled = true;
            uploadButton.Enabled = true;
            enterKeysButton.Enabled = true;
            if (_publicKey == null || _privateKey == null || _n == null || textArea.Text == "") return;
            decryptButton.Enabled = true;
            encryptButton.Enabled = true;
            attackButton.Enabled = true;
        }

        private async void AttackButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var input = textArea.Text;
            var inputInt = BigInteger.Parse(input);
            var output = await Task<Tuple<string,BigInteger>>.Factory
                .StartNew(() => RSA.RSAAttack(inputInt, _publicKey, _n));
            textArea.Text = output.Item1;
            EnableButtons();

        }

        private void EnterKeysButton_Click(object sender, EventArgs e)
        {
            _publicKey = BigInteger.Parse(publicKeyTextBox.Text);
            _privateKey = BigInteger.Parse(privateKeyTextBox.Text);
            _n = BigInteger.Parse(nTextBox.Text);
        }

        private void KeyGenAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var keyGenForm = new KeyGenForm();
            keyGenForm.ShowDialog();
        }
    }
}
