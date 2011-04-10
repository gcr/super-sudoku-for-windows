using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperSudoku
{
    /// <summary>
    /// Normal TextBoxes don't capture tabs. Instead, Windows does special (and incorrect)
    /// things to change focus. We're doing all that ourselves so we want to
    /// catch tabs.
    /// </summary>
    public partial class TabbityTextBox : TextBox
    {
        protected override bool IsInputKey(Keys keyData)
        {
            // this is so that our text boxes can capture tab keys.
            if (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift))
            {
                return true;
            }
            return base.IsInputKey(keyData);
        }
    }
}
