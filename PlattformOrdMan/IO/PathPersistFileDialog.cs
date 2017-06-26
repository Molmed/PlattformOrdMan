using System;
using System.Windows.Forms;

namespace Molmed.PlattformOrdMan.IO
{
    public class PathPersistFileDialog
    {
        public enum PathPersistFileDialogModes
        {
            OpenFileDialog,
            SaveFileDialog,
        }

        private String MyFileName;
        private String MyFilter;
        private String MyTitle;
        private PathPersistFileDialogModes MyFileDialogMode;

        public PathPersistFileDialog(PathPersistFileDialogModes fileDialogMode)
            : base()
        {
            MyFileDialogMode = fileDialogMode;
            MyFileName = "";
            MyFilter = "";
            MyTitle = "Select file";
        }

        public String FileName
        {
            get
            {
                return MyFileName;
            }
            set
            {
                MyFileName = value;
            }
        }

        public String Filter
        {
            get
            {
                return MyFilter;
            }
            set
            {
                MyFilter = value;
            }
        }

        public String Title
        {
            get
            {
                return MyTitle;
            }
            set
            {
                MyTitle = value;
            }
        }

        public DialogResult ShowDialog()
        {
            FileDialog fileDialog;

            if (MyFileDialogMode == PathPersistFileDialogModes.OpenFileDialog)
            {
                fileDialog = new OpenFileDialog();
            }
            else
            {
                fileDialog = new SaveFileDialog();
            }
            fileDialog.Filter = MyFilter;
            fileDialog.FileName = MyFileName;
            fileDialog.Title = MyTitle;
            fileDialog.InitialDirectory = Config.GetLatestDirectoryPath();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                MyFileName = fileDialog.FileName.Trim();
                if (MyFileName != "")
                {
                    // Store the path as the most recently used.
                    Config.SetLatestDirectoryPath(System.IO.Path.GetDirectoryName(MyFileName));
                }
                return DialogResult.OK;
            }
            else
            {
                MyFileName = "";
                return DialogResult.Cancel;
            }
        }
    }
}
