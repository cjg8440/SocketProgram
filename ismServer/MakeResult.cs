using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ismServer
{
    class MakeResult
    {
        private Excel.Application excelApp = null;
        private Excel.Workbook wb = null;
        private Excel.Worksheet ws = null;
        private int win_host;



        public MakeResult()
        {
            win_host = 0;
        }

        public void Analize()
        {
            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Open(Directory.GetCurrentDirectory().ToString() + "\\default\\default.xlsx");
                ws = wb.Worksheets.get_Item("별첨3_WIN_구미") as Excel.Worksheet;


                String[] file_path = Directory.GetFiles(Directory.GetCurrentDirectory().ToString() + "\\prt");

                foreach (String s in file_path)
                {
                    if (s.Split('\\')[s.Split('\\').Length - 1].Split('_')[1].Equals("WIN"))
                    {
                        Analize_WIN(s);
                    }
                }

                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
                ws.Activate();



                wb.SaveAs(@"D:\개발연습\C#\SocketProgram\ismServer\bin\Debug\_test.xlsx", Excel.XlFileFormat.xlWorkbookDefault);
                wb.Close(true);
                excelApp.Quit();

            }catch(Exception E)
            {
                System.Console.WriteLine(E.ToString());
            }
            finally{

                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);

            }

        }

        private void Analize_UNIX()
        {

        }

        private void Analize_LINUX()
        {

        }

        private void Analize_WIN(String s)
        {
            FileStream open_File = new FileStream(s, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(open_File, Encoding.Default);

            ws.Activate();

            int count = 0;

            if (win_host != 0)
            {
                ((Excel.Range)ws.Columns[6 + win_host]).Insert();
                ws.get_Range(ws.Cells[87, 5 + win_host], ws.Cells[88, 5 + win_host]).Copy();
                ((Excel.Range)ws.Cells[87, 6 + win_host]).Select();
                ws.Paste();
            }

            ws.Cells[3, 6 + win_host] = win_host + 1;
            ws.Cells[4, 6 + win_host] = s.Split('_')[2];

            System.Console.WriteLine(s);

            while (!reader.EndOfStream)
            {
                String[] temp = reader.ReadLine().ToString().Split(' ');

                if (temp[0].Length > 13 && temp[0].Substring(0, 6).Equals("OS-WIN"))
                {
                    if (temp[temp.Length - 1].Substring(2, 4).Equals("PASS"))
                        ws.Cells[9 + count, 6 + win_host] = "양호";
                    else
                    {
                        ws.Cells[9 + count, 6 + win_host] = "예외합의";
                    }
                    
                    count++;
                }

                if (count == 70)
                {
                    break;
                }
            }

            win_host++;

            ((Excel.Range)ws.Cells[9,6]).Select();
        }

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
