using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlashCardGame.Modules.Game.Control
{
    /// <summary>
    /// http://wpfknowledge.blogspot.com/2014/11/numeric-text-box.html
    /// </summary>
    public class NumericTextBox : TextBox
    {
        #region Properties

        /// <summary>
        /// Dependency property to store the decimal is allowed to be entered in the textbox
        /// </summary>
        public static readonly DependencyProperty IsDecimalAllowedProperty =
            DependencyProperty.Register("IsDecimalAllowed", typeof(Boolean), typeof(NumericTextBox), new PropertyMetadata(false));

        /// <summary>
        /// Dependency property to store the decimal is allowed to be entered in the textbox
        /// </summary>
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(int), typeof(NumericTextBox), new PropertyMetadata(0));

        /// <summary>
        /// Static Constructor
        /// </summary>
        static NumericTextBox()
        {
        }

        /// <summary>
        /// Gets or sets the character to be used as decimal separator
        /// </summary>
        public string DecimalSeparator { get; set; }

        /// <summary>
        /// Gets or sets the mask to apply to the textbox
        /// </summary>
        public Boolean IsDecimalAllowed
        {
            get { return (Boolean)GetValue(IsDecimalAllowedProperty); }
            set { SetValue(IsDecimalAllowedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the mask to apply to the textbox
        /// </summary>
        public int Scale
        {
            get { return (int)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        #endregion Properties

        /// <summary>
        /// To check the character enetered
        /// </summary>
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
            if (!e.Handled)
            {
                e.Handled = !MaxLengthReached(e);
            }
            base.OnPreviewTextInput(e);
        }

        /// <summary>
        ///To check if numbers entered are all valid numeric numbers
        /// </summary>
        private bool AreAllValidNumericChars(string str)
        {
            if (string.IsNullOrEmpty(DecimalSeparator))
                DecimalSeparator = ".";

            bool ret = true;
            if (str == System.Globalization.NumberFormatInfo.CurrentInfo.NegativeSign |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PositiveSign)
                return ret;
            if (IsDecimalAllowed && str == DecimalSeparator)
                return ret;

            int l = str.Length;
            for (int i = 0; i < l; i++)
            {
                char ch = str[i];
                ret &= Char.IsDigit(ch);
            }

            return ret;
        }

        /// <summary>
        /// This method was added to prevent arithmetic overflows while saving in db on decimal part.
        /// </summary>
        private bool MaxLengthReached(TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)e.OriginalSource;
            int precision = textBox.MaxLength - Scale - 2;

            string textToValidate = textBox.Text.Insert(textBox.CaretIndex, e.Text).Replace("-", "");
            string[] numericValues = textToValidate.Split(Convert.ToChar(DecimalSeparator));

            if ((numericValues.Length <= 2) && (numericValues[0].Length <= precision) && ((numericValues.Length == 1) || (numericValues[1].Length <= Scale)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}