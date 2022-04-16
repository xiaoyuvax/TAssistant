using GoogleTranslateFreeApi;

namespace TAssistant
{
    public partial class frmTA : Form
    {
        public frmTA()
        {
            InitializeComponent();



        }

        public async void Translate(string oText)
        {
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

            textBox1.Text = resultMerged;
            this.BringToFront();

        }

        private string LastText;

        private void button1_Click(object sender, EventArgs e)
        {
            TranslateClipBoard();

        }

        private void TranslateClipBoard()
        {
            var newText = Clipboard.GetText().Replace("#", "").Replace("@", "").Replace("\r\n\r\n", "\r\n");

            if (LastText != newText) Translate(LastText = newText);
        }

        private void frmTA_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TranslateClipBoard();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox2.Checked;
        }
    }
}