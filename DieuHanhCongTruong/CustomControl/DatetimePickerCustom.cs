using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace VNRaPaBomMin
{
    public class DatetimePickerCustom : DateTimePicker
    {
        public DatetimePickerCustom()
        {
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "dd/MM/yyyy";
            this.ShowUpDown = true;
        }
    }
}