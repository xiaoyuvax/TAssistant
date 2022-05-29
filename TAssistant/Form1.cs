using GoogleTranslateFreeApi;

namespace TAssistant
{
    public partial class frmTA : Form
    {
        private string LastText;

        public frmTA()
        {
            InitializeComponent();
        }

        public async void Translate(string oText)
        {
            if (string.IsNullOrEmpty(oText.Trim())) return;
            var translator = new GoogleTranslator();
            Language from = Language.Auto;
            Language to = GoogleTranslator.GetLanguageByName("Chinese Simplified");

            TranslationResult result = await translator.TranslateLiteAsync(oText, from, to);

            //The result is separated by the suggestions and the '\n' symbols
            string[] resultSeparated = result.FragmentedTranslation;

            //You can get all text using MergedTranslation property
            string resultMerged = result.MergedTranslation;

            //There is also original text transcription
            string transcription = result.TranslatedTextTranscription;

            textBox1.Text = resultMerged.Replace(" ", "");
            this.BringToFront();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TranslateClipBoard();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox2.Checked;
        }

        private void frmTA_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox1.Text);

            }
            catch
            {
                try
                {
                    Clipboard.SetText(textBox1.Text);

                }
                catch { }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TranslateClipBoard();
        }

        private void TranslateClipBoard()
        {
            var newText = Clipboard.GetText().Replace("#", "").Replace(",", "£¬").Replace(".", "¡£").Replace(":", "£º").Replace("@", "").Replace("\r\n\r\n", "\r\n");

            if (LastText != newText)
                if (chkTrans.Checked) Translate(LastText = newText);
                else textBox1.Text = LastText = newText;
        }
    }
}