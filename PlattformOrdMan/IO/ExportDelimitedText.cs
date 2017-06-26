using System;
using System.Text;

namespace Molmed.PlattformOrdMan.IO
{
    public class ExportDelimitedText : Export
    {
        private const Int32 NO_COLUMN_COUNT = -1;

        private Int32 MyColumnCount;
        private Int32 MyColumnIndex;
        private String MyDelimiter;
        private String MyFilter;
        private StringBuilder MyRow;

        public ExportDelimitedText()
            : this("\t", NO_COLUMN_COUNT)
        {
        }

        public ExportDelimitedText(String delimiter)
            : this(delimiter, NO_COLUMN_COUNT)
        {
        }

        public ExportDelimitedText(String delimiter, Int32 columnCount)
            : base()
        {
            MyDelimiter = delimiter;
            if (columnCount > 0)
            {
                MyColumnCount = columnCount;
            }
            else
            {
                MyColumnCount = NO_COLUMN_COUNT;
            }
            MyColumnIndex = 0;
            MyRow = null;
            MyFilter = "Text file (*.txt)|*.txt";
        }

        protected override String GetFilter()
        {
            return MyFilter;
        }

        public override void CloseFile()
        {
            EndRow();
            base.CloseFile();
        }

        public void EndRow()
        {
            if (MyRow != null)
            {
                WriteLine(MyRow.ToString());
                MyRow = null;
                MyColumnIndex = 0;
            }
        }

        public void SetFilter(String filter)
        {
            MyFilter = filter;
        }

        public void StartRow()
        {
            // Write previouse row. Just in case.
            EndRow();

            MyRow = new StringBuilder();
            MyColumnIndex = 0;
        }

        public void WriteEmptyLine()
        {
            MyStreamWriter.WriteLine("");
        }

        public void WriteItem(String item)
        {
            if (MyRow == null)
            {
                StartRow();
            }
            if (MyColumnIndex > 0)
            {
                MyRow.Append(MyDelimiter);
            }
            MyRow.Append(item);
            if (++MyColumnIndex == MyColumnCount)
            {
                EndRow();
            }
        }

        public void WriteRestOfLine()
        {
            while (IsNotNull(MyRow))
            {
                WriteItem("");
            }
        }
    }
}
