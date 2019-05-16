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
            EnableButtons();
        }

        private async void EncryptButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var input = textArea.Text;
            //var inputInt = BigInteger.Parse(input);
            var inputInt = new BigInteger(Encoding.ASCII.GetBytes(input));
            var output = await Task<BigInteger>.Factory
                .StartNew(() => Crypto.RSAEncrypt(inputInt, _publicKey, _n));
            textArea.Text = output+"";
            EnableButtons();
        }
        private async void DecryptButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var inputInt = BigInteger.Parse(textArea.Text);

            var output = await Task<BigInteger>.Factory
                .StartNew(() => Crypto.RSADecrypt(inputInt, _privateKey, _n));
            textArea.Text = Encoding.ASCII.GetString(output.ToByteArray());
            EnableButtons();
        }

        private async void GenerateButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            generateButton.Enabled = false;
            var keys = await Task<Tuple<BigInteger, BigInteger, BigInteger>>.Factory
                .StartNew(() => Crypto.GenerateKeys(_size));

            _publicKey = keys.Item1;
            _privateKey = keys.Item2;
            _n = keys.Item3;
            EnableButtons();
        }

        private void DisableButtons()
        {
            generateButton.Enabled = false;
            encryptButton.Enabled = false;
            decryptButton.Enabled = false;
            uploadButton.Enabled = false;
        }

        private void EnableButtons()
        {
            generateButton.Enabled = true;
            uploadButton.Enabled = true;
            if (_publicKey == null || _privateKey == null || _n == null || textArea.Text == "") return;
            decryptButton.Enabled = true;
            encryptButton.Enabled = true;
        }
    }
}
