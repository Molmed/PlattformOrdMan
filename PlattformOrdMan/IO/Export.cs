using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan;

namespace Molmed.PlattformOrdMan.IO
{
    public class Export : PlattformOrdManBase
    {
        protected StreamWriter MyStreamWriter;
        private String MyDefaultFileName;
        private String MyDefaultFilePath;
        private String MyFilePath;

        public Export()
            : base()
        {
            MyStreamWriter = null;
            MyDefaultFileName = "";
            MyDefaultFilePath = null;
        }

        public virtual void CloseFile()
        {
            if (MyStreamWriter != null)
            {
                MyStreamWriter.Close();
                MyStreamWriter = null;
            }
        }

        public virtual String GetDefaultFileName()
        {
            return MyDefaultFileName;
        }

        public String GetFileName(Boolean includeExtension)
        {
            if (includeExtension)
            {
                return Path.GetFileName(MyFilePath);
            }
            else
            {
                return Path.GetFileNameWithoutExtension(MyFilePath);
            }
        }

        public String GetFilePath()
        {
            return MyFilePath;
        }

        protected virtual String GetFilter()
        {
            return "All files (*.*)|*.*";
        }

        protected Boolean IsFileOpen()
        {
            return MyStreamWriter != null;
        }

        public virtual Boolean OpenFile()
        {
            return OpenFile(null);
        }

        public virtual Boolean OpenFile(String fileDialogTitle)
        {
            Boolean isFileOpen = false;
            PathPersistFileDialog openFileDialog;

            if (MyDefaultFilePath == null)
            {
                // Ask for file to save to;
                openFileDialog = new PathPersistFileDialog(PathPersistFileDialog.PathPersistFileDialogModes.SaveFileDialog);
                openFileDialog.Filter = GetFilter();
                openFileDialog.FileName = GetDefaultFileName();
                if (fileDialogTitle != null)
                {
                    openFileDialog.Title = fileDialogTitle;
                }
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MyFilePath = openFileDialog.FileName;
                    MyStreamWriter = new StreamWriter(MyFilePath, false);
                    isFileOpen = true;
                }
            }
            else
            {
                // Save to specified file.
                MyFilePath = MyDefaultFilePath;
                MyStreamWriter = new StreamWriter(MyFilePath, false);
                isFileOpen = true;
            }
            return isFileOpen;
        }

        public void SetDefaultFileName(String defaultFileName)
        {
            MyDefaultFileName = defaultFileName;
        }

        public void SetDefaultFilePath(String defaultFilePath)
        {
            MyDefaultFilePath = defaultFilePath;
        }

        public void Write(String text)
        {
            MyStreamWriter.Write(text);
        }

        public void WriteLine(String line)
        {
            MyStreamWriter.WriteLine(line);
        }
    }
}
