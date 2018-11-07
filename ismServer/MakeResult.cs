using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
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

                Analize_Result();

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

            /* 이호[사원] 님의 말 (오후 12:23) :  
            public delegate void logWrite(string txt);
            public static event logWrite;

            로그 쓸 부분에 이렇게 선언하고
            쓸때는 logWrite(text);
            이렇게
            그리고 폼에서 이벤트 추가
            이호[사원] 님의 말 (오후 12:24) :  
            아 다시
            이호[사원] 님의 말 (오후 12:26) :  
            선언
            public delegate void logWrite(string txt);
            public static event logWrite logW;

            사용 
            logW(text);

            이벤트
            클래스명.logW += new 클래스명.logWrite(메소드명);
            이호[사원] 님의 말 (오후 12:27) :  
            private void 메소드명(string txt){
            textbox.appendText(txt);

            }*/
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

            int count = 0;

            ws.Cells[3, 6 + win_host] = win_host + 1;
            ws.Cells[4, 6 + win_host] = s.Split('_')[2];

            while (!reader.EndOfStream)
            {
                String[] temp = reader.ReadLine().ToString().Split(' ');

                if (temp[0].Length > 13 && temp[0].Substring(0, 6).Equals("OS-WIN"))
                {
                    if (temp[temp.Length - 1].Substring(2, 4).Equals("PASS"))
                        ws.Cells[9 + count, 6 + win_host] = "양호";
                    else
                    {
                        ws.Cells[9 + count, 6 + win_host] = "조치예외";
                    }
                    
                    count++;
                }

                if (count == 70)
                {
                    break;
                }
            }

            win_host++;
        }

        private void Analize_Result()
        {
            System.Console.WriteLine("Analize_Result() 시작");

            ws.Cells[8, 6 + win_host] = "양호";
            ws.Cells[8, 7 + win_host] = "예외합의";
            ws.Cells[8, 8 + win_host] = "예외요청";
            ws.Cells[8, 9 + win_host] = "합계";

            for(int inx = 0; inx < 78; inx++)
            {
                System.Console.WriteLine(inx);

                ws.Cells[9 + inx, 6 + win_host]= "=COUNTIF('양호')";
            }
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
