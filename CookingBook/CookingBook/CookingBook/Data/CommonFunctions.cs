using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Windows.Input;
using System.ComponentModel;
using System.IO;

namespace CookingBook.Data
{
    public static class CommonFunctions
    {
        public static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
