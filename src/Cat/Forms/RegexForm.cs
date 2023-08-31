using System;
using System.Text.RegularExpressions;

namespace WinkingCat
{
    public partial class RegexForm : BaseForm
    {
        private Regex reg;

        public RegexForm()
        {
            InitializeComponent();
            base.RegisterEvents();
        }

        private void btnRunCheck_Click(object sender, EventArgs e)
        {
            try
            {
                reg = new Regex(textBox1.Text);

                lRegexMatch.Text = reg.IsMatch(textBox2.Text).ToString();
                tbException.Text = "";
            }
            catch (Exception ex)
            {
                lRegexMatch.Text = "Null";
                tbException.Text = ex.ToString();
            }
        }
    }
}
