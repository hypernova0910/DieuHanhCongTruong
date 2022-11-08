using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNRaPaBomMin
{
    public class Int32TextBoxCustom : ValidatingTextBoxCustom
    {
        protected override void OnTextValidating(object sender, TextValidatingEventArgs e)
        {
            if (e.NewText.Contains(' '))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = !double.TryParse(e.NewText, out double i);
            if (e.Cancel == false)
                if (i != 0)
                {
                    if (e.NewText.Contains('.'))
                        return;

                    this.Text = i.ToString();
                }
        }
    }
}